﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using KSP.UI;
using KSP.UI.Screens;
namespace JanitorsCloset
{
    class ShowRenamed : MonoBehaviour
    {
        //const int WIDTH = Screen.width - 200;
        const int HEIGHT = 500;

        List<string> renamedList;

        Rect _windowRect = new Rect()
        {
            xMin = 0,
            xMax = UnityEngine.Screen.width - 300,
            yMin = 0,
            yMax = HEIGHT
        };


        public static ShowRenamed Instance { get; private set; }

        void Awake()
        {
            Log.Info("ShowRenamed Awake()");
            this.enabled = false;
            Instance = this;
            _windowRect.center = new Vector2(UnityEngine.Screen.width * 0.5f, UnityEngine.Screen.height * 0.5f);
        }

        public void Show()
        {
            enabled = true;
            renamedList = new List<string>();
        }

        public void addLine(string str)
        {
            renamedList.Add(str);
        }

        public bool isEnabled()
        {
            return this.enabled;
        }

        void CloseWindow()
        {
            this.enabled = false;

            Log.Info("ShowRenamed.CloseWindow enabled: " + this.enabled.ToString());
        }

        void OnGUI()
        {
            if (isEnabled())
            {
                var tstyle = new GUIStyle(GUI.skin.window);
                _windowRect = GUILayout.Window(this.GetInstanceID(), _windowRect, WindowContent, "Show PermaPruned Parts", tstyle);
            }
        }

        Vector2 sitesScrollPosition;
        const int LINEHEIGHT = 30;

        const int MODNAMEWIDTH = 225;
        const int WHEREWIDTH = 55;

        private Rect innerCoords;

        void WindowContent(int windowID)
        {
            innerCoords = new Rect(0, LINEHEIGHT, UnityEngine.Screen.width - 300, HEIGHT - 2 * LINEHEIGHT);
            
            GUILayout.BeginVertical();

#if false
            sitesScrollPosition = GUILayout.BeginScrollView(sitesScrollPosition, false, true, GUILayout.Height(HEIGHT - LINEHEIGHT));
            foreach (var blp in renamedList)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(blp);
                
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
#endif
            string t = "";
            foreach (var blp in renamedList)
                t += blp + "\n";
//            GUILayout.BeginArea(innerCoords);
            sitesScrollPosition = GUILayout.BeginScrollView(sitesScrollPosition, false, true, GUILayout.Height(HEIGHT - LINEHEIGHT));
            //GUI.enabled = false;
            GUILayout.Label(t);
            //GUI.enabled = true;
            GUILayout.EndScrollView();
 //           GUILayout.EndArea();

            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (!PermaPruneWindow.Instance.permapruneInProgress)
            {
                if (GUILayout.Button(" Close "))
                {
                    CloseWindow();
                }
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Copy to clipboard"))
                {
                    GUIUtility.systemCopyBuffer = t;
                }
            }
            else
            {
                if (GUILayout.Button(" Cancel "))
                {
                    PermaPruneWindow.Instance.stopPruner();
                }

            }
            
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            GUI.DragWindow();
        }
    }
}
