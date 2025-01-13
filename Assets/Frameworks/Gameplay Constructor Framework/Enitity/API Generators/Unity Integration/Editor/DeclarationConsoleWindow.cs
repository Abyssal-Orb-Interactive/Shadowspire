using System;
using System.Collections.Generic;
using System.Linq;
using GameplayConstructorFramework.APIs;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameplayConstructorFramework.Unity.Editor
{
    public class DeclarationConsoleWindow : EditorWindow
    {
        private WindowState _windowState;

        private Section _activeSection = null;
        private ListView _declaredPairsContainer = null;
        private ListView _sectionsContainer = null;
        private TextField _keyField = null;
        private TextField _typeField = null;
        private APIsGenerator _apisGenerator = null;
        private APIGeneratorConfig _config = null;

        [MenuItem("Tools/Declaration Console")]
        public static void ShowWindow()
        {
            var window = GetWindow<DeclarationConsoleWindow>();
            window.titleContent = new GUIContent("Declaration Console");
            window.Show();
        }

        public void CreateGUI()
        {
            _config = AssetDatabase.LoadAssetAtPath<APIGeneratorConfig>("Assets/Content/Configs/APIGeneratorConfig.asset");
    
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UXML/DeclariationConsole.uxml");
            visualTree.CloneTree(rootVisualElement);

            _declaredPairsContainer = rootVisualElement.Q<ListView>("pairs");
            _sectionsContainer = rootVisualElement.Q<ListView>("sections");

            _keyField = rootVisualElement.Q<TextField>("new-data-key-field");
            _typeField = rootVisualElement.Q<TextField>("new-data-type-field");
            var addPairButton = rootVisualElement.Q<Button>("add-pair-button");
            addPairButton.clicked += AddPairToActiveSection;

            var sectionField = rootVisualElement.Q<TextField>("new-section-name-field");
            var addSectionButton = rootVisualElement.Q<Button>("add-section-button");
            addSectionButton.clicked += () => AddSection(sectionField.value);

            var behavioursUpdateButton = rootVisualElement.Q<Button>("update-behaviour-button");
            behavioursUpdateButton.clicked += UpdateBehaviours;

            LoadWindowState();
            IEnumerable<DeclaratoryPair> declaredPairs = LoadAPIsGeneratorState();

            _apisGenerator = new APIsGenerator(_config, declaredPairs);
            
            SetUpSectionsListView();
            SetUpPairsListView();
            
            SynchronizeDeclaredPairsAndView();

            if (_windowState.Sections.Count == 0)
            {
                AddDefaultGlobalSection();
            }

            SelectGlobalSection();
        }

        private void AddPairToActiveSection()
        {
            if (_activeSection == null || string.IsNullOrEmpty(_keyField.value)) return;
            if(!_apisGenerator.TryDeclareDataKeyTypePair(new DeclaratoryPair(_keyField.value, _typeField.value))) return;
            //AssemblyDefinitionUpdater.UpdateForAdding(_config.APIsFolderPath, _typeField.value);

            _activeSection.Pairs.Add(new DeclaratoryPair
            {
                Key = _keyField.value,
                Type = _typeField.value
            });
            
            RedrawPairs();
            
            SaveWindowState();
            SaveAPIsGeneratorState();
        }

        private void AddSection(string sectionName)
        {
            if (string.IsNullOrEmpty(sectionName)) return;

            var newSection = new Section { SectionName = sectionName };
            _windowState.Sections.Add(newSection);
            RedrawSections();
            SaveWindowState();
        }

        private void RedrawSections()
        {
            var sectionsListView = _sectionsContainer;
            sectionsListView.itemsSource = _windowState.Sections;
            sectionsListView.Rebuild(); 
        }
        
        private void RedrawPairs()
        {
            var pairsListView = _declaredPairsContainer;
            pairsListView.itemsSource = _activeSection?.Pairs ?? new List<DeclaratoryPair>();
            pairsListView.Rebuild();
            pairsListView.ScrollToItemById(0);
        }


        private void SelectSection(Section section)
        {
            _activeSection = section;
            RedrawPairs();
        }
        
        private void SynchronizeDeclaredPairsAndView()
        {
            var allPairsInConsole = _windowState.Sections.SelectMany(section => section.Pairs).ToList();
    
            var allPairsInAPIsGenerator = _apisGenerator.DeclaredPairs.ToList();

            var globalSection = _windowState.Sections.FirstOrDefault(s => s.SectionName == "Global");
            if (globalSection == null)
            {
                globalSection = new Section { SectionName = "Global", Pairs = new List<DeclaratoryPair>() };
                _windowState.Sections.Add(globalSection);
            }

            foreach (var consolePair in allPairsInConsole.Where(consolePair => !allPairsInAPIsGenerator.Any(p => p.Key == consolePair.Key && p.Type == consolePair.Type)))
            {
                _apisGenerator.TryDeclareDataKeyTypePair(consolePair);
            }

            foreach (var apisPair in allPairsInAPIsGenerator.Where(apisPair => !allPairsInConsole.Any(p => p.Key == apisPair.Key && p.Type == apisPair.Type)))
            {
                globalSection.Pairs.Add(apisPair);
            }

            SaveWindowState();
            SaveAPIsGeneratorState();

            RedrawSections();
            RedrawPairs();
        }
        
        private void SetUpSectionsListView()
        {
            var sectionsListView = _sectionsContainer;
            
            sectionsListView.makeItem = () =>
            {
                var container = new VisualElement
                {
                    style =
                    {
                        flexDirection = FlexDirection.Row
                    }
                };

                var nameField = new TextField { name = "section-name-field", style = { flexGrow = 1, marginRight = 5 } };
                var deleteButton = new Button { name = "delete-button", text = "Delete", style = { flexGrow = 0 } };

                container.Add(nameField);
                container.Add(deleteButton);

                return container;
            };
            
            sectionsListView.bindItem = (element, i) =>
            {
                if (i >= _windowState.Sections.Count) return;
                
                var section = _windowState.Sections[i];
                
                var nameField = element.Q<TextField>("section-name-field");
                var deleteButton = element.Q<Button>("delete-button");
                
                nameField.RegisterValueChangedCallback(OnSectionNameChanged(section));

                if (nameField.value != section.SectionName)
                {
                    nameField.SetValueWithoutNotify(section.SectionName);
                }
                
                if (section.SectionName == "Global")
                {
                    nameField.SetEnabled(false);
                    deleteButton.SetEnabled(false);
                }
                else
                {
                    nameField.SetEnabled(true);
                    deleteButton.SetEnabled(true);
                }
                
                deleteButton.clicked += () => OnSectionDelete(i);
            };

            sectionsListView.onSelectionChange += selectedItems =>
            {
                if (selectedItems.FirstOrDefault() is Section selectedSection)
                {
                    SelectSection(selectedSection);
                }
            };
    
            sectionsListView.itemsSource = _windowState.Sections;
            sectionsListView.fixedItemHeight = 40;
            sectionsListView.Rebuild();
        }
        
        private EventCallback<ChangeEvent<string>> OnSectionNameChanged(Section section)
        {
            return evt =>
            {
                section.SectionName = evt.newValue;
                
                SaveWindowState();
                
                RedrawSections();
            };
        }
        
        private void OnSectionDelete(int index)
        {
            if (index < 0 || index >= _windowState.Sections.Count) return;

            var sectionToDelete = _windowState.Sections[index];

            var globalSection = _windowState.Sections.First(s => s.SectionName == "Global");
            globalSection.Pairs.AddRange(sectionToDelete.Pairs);

            if (sectionToDelete.SectionName == "Global") return;
            
            _windowState.Sections.RemoveAt(index);
            SaveWindowState();
            RedrawSections();
            RedrawPairs();
        }
        
        private void SetUpPairsListView()
        {
            var pairsListView = _declaredPairsContainer;

            pairsListView.makeItem = () =>
            {
                var container = new VisualElement
                {
                    style =
                    {
                        flexDirection = FlexDirection.Row
                    }
                };

                var keyField = new TextField { name = "key-field", style = { flexGrow = 1, marginRight = 5 } };
                var typeField = new TextField { name = "type-field", style = { flexGrow = 1, marginRight = 5 } };
                var deleteButton = new Button { name = "delete-button", text = "Delete", style = { flexGrow = 0 } };

                container.Add(keyField);
                container.Add(typeField);
                container.Add(deleteButton);

                return container;
            };
    
            pairsListView.bindItem = (element, i) =>
            {
                if (_activeSection == null || i >= _activeSection.Pairs.Count) return;
                
                var pair = _activeSection.Pairs[i];
                
                var keyField = element.Q<TextField>("key-field");
                var typeField = element.Q<TextField>("type-field");
                var deleteButton = element.Q<Button>("delete-button");
                
                keyField.UnregisterValueChangedCallback(OnKeyChanged(pair));
                typeField.UnregisterValueChangedCallback(OnTypeChanged(pair));
                
                keyField.value = pair.Key;
                typeField.value = pair.Type;

                keyField.RegisterValueChangedCallback(OnKeyChanged(pair)); 
                typeField.RegisterValueChangedCallback(OnTypeChanged(pair));
                
                deleteButton.clicked += () =>
                {
                    keyField.UnregisterValueChangedCallback(OnKeyChanged(pair));
                    typeField.UnregisterValueChangedCallback(OnTypeChanged(pair));
                    //AssemblyDefinitionUpdater.UpdateForRemoving(_config.APIsFolderPath, pair.Type);
                    _apisGenerator.TryDeleteDataKeyTypePair(new DeclaratoryPair(pair.Key, pair.Type));
                    _activeSection.Pairs.RemoveAt(i);
                    UpdateBehaviours();
                    RedrawPairs();
                };
            };

            pairsListView.itemsSource = _activeSection?.Pairs ?? new List<DeclaratoryPair>();
            pairsListView.fixedItemHeight = 40;
            pairsListView.Rebuild();
        }

        private static EventCallback<ChangeEvent<string>> OnTypeChanged(DeclaratoryPair pair)
        {
            return evt =>
            {
                pair.Type = evt.newValue;
            };
        }

        private static EventCallback<ChangeEvent<string>> OnKeyChanged(DeclaratoryPair pair)
        {
            return evt =>
            {
                pair.Key = evt.newValue;
            };
        }
        
        private void UpdateBehaviours()
        {
            _apisGenerator.SaveChanges();
        }
        
        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            AssemblyReloadEvents.beforeAssemblyReload += SaveState;
        }
        
        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state is PlayModeStateChange.ExitingEditMode or PlayModeStateChange.ExitingPlayMode)
            {
                SaveState();
            }
        }
        
        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            AssemblyReloadEvents.beforeAssemblyReload -= SaveState;
            SaveState();
        }

        private void SaveState()
        {
            SaveWindowState();
            SaveAPIsGeneratorState();
        }

        private void AddDefaultGlobalSection()
        {
            var globalSection = new Section { SectionName = "Global", Pairs = new List<DeclaratoryPair>() };
            _windowState.Sections.Add(globalSection);

            _activeSection = globalSection;

            RedrawSections();
            RedrawPairs();
        }

        private void SaveWindowState()
        {
            var json = JsonUtility.ToJson(_windowState, true);
            System.IO.File.WriteAllText(GetSaveFilePath("WindowState.json"), json);
        }
        
        private string GetSaveFilePath(string fileName)
        {
            return System.IO.Path.Combine(Application.persistentDataPath, fileName);
        }

        private void LoadWindowState()
        {
            var path = GetSaveFilePath("WindowState.json");
            if (System.IO.File.Exists(path))
            {
                var json = System.IO.File.ReadAllText(path);
                _windowState = JsonUtility.FromJson<WindowState>(json);
            }
            else
            {
                _windowState = new WindowState
                {
                    Sections = new List<Section>
                    {
                        new() { SectionName = "Global", Pairs = new List<DeclaratoryPair>() }
                    }
                };
            }
        }

        private void SaveAPIsGeneratorState()
        {
            if(_apisGenerator?.DeclaredPairs == null) return;
            
            var declaredPairs = _apisGenerator.DeclaredPairs.ToList();
            var json = JsonUtility.ToJson(new SerializationHelper<List<DeclaratoryPair>>{Value = declaredPairs}, true);
            System.IO.File.WriteAllText(GetSaveFilePath("APIsGeneratorState.json"), json);
        }

        private IEnumerable<DeclaratoryPair> LoadAPIsGeneratorState()
        {
            var path = GetSaveFilePath("APIsGeneratorState.json");
            
            if (!System.IO.File.Exists(path)) return new List<DeclaratoryPair>();
            
            var json = System.IO.File.ReadAllText(path);
            var wrapper = JsonUtility.FromJson<SerializationHelper<List<DeclaratoryPair>>>(json);

            return wrapper.Value ?? new List<DeclaratoryPair>();
        }
        
        private void SelectGlobalSection()
        {
            var globalSection = _windowState.Sections.FirstOrDefault(s => s.SectionName == "Global");

            if (globalSection == null) return;
            
            _activeSection = globalSection;

            RedrawPairs();
            RedrawSections();

            var index = _windowState.Sections.IndexOf(globalSection);
            _sectionsContainer.selectedIndex = index;
            _sectionsContainer.ScrollToItem(index);
        }

        [Serializable]
        private struct WindowState
        {
            public List<Section> Sections;
        }

        [Serializable]
        private struct SerializationHelper<T>
        {
            public T Value;
        }
    }
}