using System;
using Rampastring.Tools;
using System.IO;
using System.Collections.Generic;

namespace ClientCore
{
    public class Translation
    {
        private const string TEXTURES = "Textures";
        private const string STRINGS = "Strings";
        private const string GAME_OPTIONS = "Game Options";
        private const string SETTINGS = "Settings";

        private static Translation _instance;

        private IniFile translationIni;

        protected SortedDictionary<string, string> texturesDictionary = new SortedDictionary<string, string>();
        protected SortedDictionary<string, string> stringsDictionary = new SortedDictionary<string, string>();
        protected SortedDictionary<string, string> gameOptionsDictionary = new SortedDictionary<string, string>();
        protected SortedDictionary<string, string> settingsDictionary = new SortedDictionary<string, string>();

        protected Translation()
        {
            string filename = ClientConfiguration.Instance.GetTranslationPath(UserINISettings.Instance.ClientTranslation);

            if (!File.Exists(ProgramConstants.GetBaseResourcePath() + filename))
                Logger.Log($"Translation: Couldn't find {filename} translation file");

            translationIni = new IniFile(ProgramConstants.GetBaseResourcePath() + filename);

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
            if (translationIni.SectionExists(TEXTURES))
                foreach (string key in translationIni.GetSectionKeys(TEXTURES))
                    texturesDictionary[key] = translationIni.GetStringValue(TEXTURES, key, String.Empty);

            if (translationIni.SectionExists(STRINGS))
                foreach (string key in translationIni.GetSectionKeys(STRINGS))
                    stringsDictionary[key] = translationIni.GetStringValue(STRINGS, key, String.Empty);

            if (translationIni.SectionExists(GAME_OPTIONS))
                foreach (string key in translationIni.GetSectionKeys(GAME_OPTIONS))
                    gameOptionsDictionary[key] = translationIni.GetStringValue(GAME_OPTIONS, key, String.Empty);

            if (translationIni.SectionExists(SETTINGS))
                foreach (string key in translationIni.GetSectionKeys(SETTINGS))
                    settingsDictionary[key] = translationIni.GetStringValue(SETTINGS, key, String.Empty);
        }

        public void GenerateTranslationIni()
        {
            if (!translationIni.SectionExists(TEXTURES))
                translationIni.AddSection(TEXTURES);

            if (!translationIni.SectionExists(STRINGS))
                translationIni.AddSection(STRINGS);

            if (!translationIni.SectionExists(GAME_OPTIONS))
                translationIni.AddSection(GAME_OPTIONS);

            if (!translationIni.SectionExists(SETTINGS))
                translationIni.AddSection(SETTINGS);


            foreach (string key in texturesDictionary.Keys)
                translationIni.SetStringValue(TEXTURES, key, texturesDictionary[key]);

            foreach (string key in stringsDictionary.Keys)
                translationIni.SetStringValue(STRINGS, key, stringsDictionary[key]);

            foreach (string key in gameOptionsDictionary.Keys)
                translationIni.SetStringValue(GAME_OPTIONS, key, gameOptionsDictionary[key]);

            foreach (string key in settingsDictionary.Keys)
                translationIni.SetStringValue(SETTINGS, key, settingsDictionary[key]);

            translationIni.WriteIniFile();
        }

        private string GetTranslationFromDictionary(SortedDictionary<string, string> dictionary, string key, string defaultValue)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, defaultValue);
            return dictionary[key];
        }

        public string GetTranslatedTexture(string key, string defaultValue)
            => GetTranslationFromDictionary(texturesDictionary, key, defaultValue);

        public string GetTranslatedString(string key, string defaultValue) 
            => GetTranslationFromDictionary(stringsDictionary, key, defaultValue);

        public string GetTranslatedGameOption(string key, string defaultValue) 
            => GetTranslationFromDictionary(gameOptionsDictionary, key, defaultValue);

        public string GetTranslatedSetting(string key, string defaultValue)
            => GetTranslationFromDictionary(settingsDictionary, key, defaultValue);
    }    
}
