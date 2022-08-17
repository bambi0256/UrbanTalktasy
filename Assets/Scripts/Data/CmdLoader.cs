using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Game;
using UnityEngine;

namespace Data
{
    public class CmdLoader : MonoBehaviour
    {
        private const string CMD_RE = @"(?<=\s*)(?<name>\w+)\s*\((?<factor>\s*(?:(?:""""[\s\S]*?"""")|[\w.\d]*?)\s*(?:,\s*(?:(?:""""[\s\S]*?"""")|[\w.\d]+?)\s*)*)\);";
        private const string CMD_SPLIT_RE = ",(?=(?:[^\"\"]*\"\"[^\"\"]*\"\")*[^\"\"]*$)";
        private const string CMD_REPLACE_RE = @"^\s*""""|""""\s*$";
        
        private new readonly string name;
        private readonly object[] factors;
        
        public bool IsEnd { get; private set; }
        public static CmdLoader runningCmd;
    
        public static List<CmdLoader> GetCmd(TextAsset textAsset, int id)
        {
            var cmd= DialogueParse.csvData[textAsset][id].cmd;

            var cmdRegex = new Regex(CMD_RE);
            var myCmd = new List<CmdLoader>();

            foreach (Match match in cmdRegex.Matches(cmd))
            {
                CmdLoader cmds = new CmdLoader(match.Groups["name"].Value, match.Groups["factor"].Value);
                myCmd.Add(cmds);
            }

            return myCmd;
        }
        
        public CmdLoader(string _name, string _factor)
        {
            name = _name;
            if (string.IsNullOrEmpty(_factor))
            {
                factors = null;
            }
            else 
            {
                var _factors = Regex.Split(_factor, CMD_SPLIT_RE);
                factors = new object[_factors.Length];
                for (var i = 0; i < factors.Length; ++i) {
                    var _stringValue = Regex.Replace(_factors[i], CMD_REPLACE_RE, "");
                    var isInt = int.TryParse(_stringValue, out var _intValue);
                    var isFloat = float.TryParse(_stringValue, out var _floatValue);
                    var isString = isFloat == false && isInt == false;
                    if (isString)
                    {
                        factors[i] = _stringValue;
                    }
                    else if (isInt)
                    {
                        if (isFloat) factors[i] = _floatValue;
                        else factors[i] = _intValue;
                    }
                }
                IsEnd = false;
            }
        }

        public void Execute(object target)
        {
            var method = typeof(IdRoutine).GetMethod(name);
            
            if (method == null)
            {
                return;
            }
            runningCmd = this;
            
            try
            {
                method.Invoke(target, factors);
            }
            catch (Exception)
            {
                var infos = method.GetParameters();
                foreach (var t in infos)
                {
                    if (t.ParameterType == typeof(float[])) {
                        var newFactors = new float[factors.Length];
                        for (var j = 0; j < newFactors.Length; ++j) {
                            newFactors[j] = (float)factors[j];
                        }
                        method.Invoke(target, new object[] { newFactors });
                        break;
                    }
                    else if (t.ParameterType == typeof(string)) {
                        var newFactors = new object[factors.Length];
                        for (var j = 0; j < newFactors.Length; ++j) {
                            newFactors[j] = $"{factors[j]}";
                        }
                        method.Invoke(target, newFactors);
                        break;
                    }
                }
            }
        }
    }
}
