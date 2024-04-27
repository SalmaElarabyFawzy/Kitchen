using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IHavePrograss 
{
     event EventHandler<OnProgressChangedArgs> OnProgressChanged;
     class OnProgressChangedArgs : EventArgs
    {
        public float Progress;
        public OnProgressChangedArgs(float progress)
        {
            Progress = progress;
        }
    }
   
}
