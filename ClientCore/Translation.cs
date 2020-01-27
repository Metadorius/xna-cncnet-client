using System;
using Rampastring.Tools;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace ClientCore
{
    public class Translation
    {
        // Code-defined UI element texts and messages
        private const string STATIC_STRINGS = "Static Strings";

        // Dynamic (INI-defined) UI element texts
        private const string INI_STRINGS = "INI Strings";
        private const string TEXTURES = "Textures";
        private const string GAME_OPTIONS = "Game Options";
        private const string GAME_MODES = "Game Modes";
        private const string SIDES = "Sides";
        private const string COLORS = "Colors";
        private const string MISSION_NAMES = "Mission Names";
        private const string MISSION_BRIEFINGS = "Mission Briefings";
        private const string MAP_NAMES = "Map Names";
        private const string MAP_BRIEFINGS = "Map Briefings";
        private const string CUSTOM_SETTINGS = "Custom Settings";
        private const string CUSTOM_COMPONENTS = "Custom Components";

        #region Static strings

        public static string GameLobby_Spectator { get; set; } = "Spectator";

        public static string General_Cancel { get; set; } = "Cancel";
        public static string General_Random { get; set; } = "Random";

        #endregion

        private static Translation _instance;

        private IniFile translationIni;

        protected Dictionary<string, SortedDictionary<string, string>> translationDictionary
            = new Dictionary<string, SortedDictionary<string, string>>();

        protected Translation()
        {
            string filename = ClientConfiguration.Instance.GetTranslationPath(UserINISettings.Instance.ClientTranslation);

            if (!File.Exists(ProgramConstants.GetBaseResourcePath() + filename))
                Logger.Log($"Translation: Couldn't find {filename} translation file");

            translationIni = new IniFile(ProgramConstants.GetBaseResourcePath() + filename);

            foreach (string section in new string[] {INI_STRINGS, TEXTURES, GAME_OPTIONS, GAME_MODES, SIDES, COLORS,
                MISSION_NAMES, MISSION_BRIEFINGS, MAP_NAMES, MAP_BRIEFINGS, CUSTOM_SETTINGS, CUSTOM_COMPONENTS})
                translationDictionary.Add(section, new SortedDictionary<string, string>());

            LoadFromTranslationIni();
        }

        /// <summary>
        /// Singleton Pattern. Returns the object of this class.
        /// </summary>
        /// <returns>The object of the Translation class.</returns>
        public static Translation Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Translation();
                return _instance;
            }
        }

        private void LoadFromTranslationIni()
        {
            foreach (var property in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static))
                if (property.PropertyType == typeof(string))
                    property.SetValue(this, translationIni.GetStringValue(STATIC_STRINGS, property.Name, (string)property.GetValue(this, null)), null);

            foreach (string section in translationDictionary.Keys)
                if (translationIni.SectionExists(section))
                    foreach (string key in translationIni.GetSectionKeys(section))
                        translationDictionary[section][key] = translationIni.GetStringValue(section, key, String.Empty);
        }

        public void GenerateTranslationIni()
        {
            if (!translationIni.SectionExists(STATIC_STRINGS))
                translationIni.AddSection(STATIC_STRINGS);

            foreach (var property in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static))
                if (property.PropertyType == typeof(string))
                    translationIni.SetStringValue(STATIC_STRINGS, property.Name, (string)property.GetValue(this, null));

            foreach (string section in translationDictionary.Keys)
            {
                if (!translationIni.SectionExists(section))
                    translationIni.AddSection(section);

                foreach (string key in translationDictionary[section].Keys)
                    translationIni.SetStringValue(section, key, translationDictionary[section][key]);
            }

            translationIni.WriteIniFile();
        }

        private string GetTranslationFromDictionary(string section, string key, string defaultValue)
        {
            if (!translationDictionary[section].ContainsKey(key))
                translationDictionary[section].Add(key, defaultValue);
            return translationDictionary[section][key];
        }

        #region Public dictionary access methods

        public string GetTranslatedIniString(string key, string defaultValue)
            => GetTranslationFromDictionary(INI_STRINGS, key, defaultValue);

        public string GetTranslatedTexture(string key, string defaultValue)
            => GetTranslationFromDictionary(TEXTURES, key, defaultValue);

        public string GetTranslatedGameOption(string key, string defaultValue) 
            => GetTranslationFromDictionary(GAME_OPTIONS, key, defaultValue);

        public string GetTranslatedGameMode(string key, string defaultValue)
            => GetTranslationFromDictionary(GAME_MODES, key, defaultValue);

        public string GetTranslatedSide(string key, string defaultValue)
            => GetTranslationFromDictionary(SIDES, key, defaultValue);

        public string GetTranslatedColor(string key, string defaultValue)
            => GetTranslationFromDictionary(COLORS, key, defaultValue);

        public string GetTranslatedMissionName(string key, string defaultValue)
            => GetTranslationFromDictionary(MISSION_NAMES, key, defaultValue);

        public string GetTranslatedMissionBriefing(string key, string defaultValue)
            => GetTranslationFromDictionary(MISSION_BRIEFINGS, key, defaultValue);

        public string GetTranslatedMapName(string key, string defaultValue)
            => GetTranslationFromDictionary(MAP_NAMES, key, defaultValue);

        public string GetTranslatedMapBriefing(string key, string defaultValue)
            => GetTranslationFromDictionary(MAP_BRIEFINGS, key, defaultValue);

        public string GetTranslatedCustomSetting(string key, string defaultValue)
            => GetTranslationFromDictionary(CUSTOM_SETTINGS, key, defaultValue);

        public string GetTranslatedCustomComponent(string key, string defaultValue)
            => GetTranslationFromDictionary(CUSTOM_COMPONENTS, key, defaultValue);

        #endregion
    }
}
