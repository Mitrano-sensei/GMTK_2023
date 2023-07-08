using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class RecorderManager : MonoBehaviour
{
    private static RecorderManager _instance;
    public static RecorderManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private Recording _superBoiRecording;
    private Recording _superGirlRecording;

    internal void ResetAll()
    {
        _superBoiRecording = new Recording(PlayerController.PlayerCharacter.SuperCapsuleBoy);
        _superGirlRecording = new Recording(PlayerController.PlayerCharacter.SuperRectangleGirl);
    }

    internal void ResetSuperBoi()
    {
        _superBoiRecording = new Recording(PlayerController.PlayerCharacter.SuperCapsuleBoy);
    }

    private void Awake()
    {
        _instance = this;
        ResetAll();
    }

    internal Recording GetRecord(PlayerController.PlayerCharacter whoAmI)
    {
        if (whoAmI == PlayerController.PlayerCharacter.SuperRectangleGirl)
        {
            return _superGirlRecording;
        } else if (whoAmI == PlayerController.PlayerCharacter.SuperCapsuleBoy)
        {
            return _superBoiRecording;
        }

        throw new Exception("Should not happen");
    }

    public class Recording
    {
        private PlayerController.PlayerCharacter _character;
        private List<TarodevController.FrameInput> _inputList;
        private int _currentFrame;

        public Recording(PlayerController.PlayerCharacter character)
        {
            _character = character;
            _inputList = new List<TarodevController.FrameInput>();
            _currentFrame = 0;
        }

        public void RecordInput(FrameInput frameInput)
        {
            _inputList.Add(frameInput);
        }

        public FrameInput ReadInput()
        {
            var frameInput = _inputList[_currentFrame];
            _currentFrame++;

            return frameInput;
        }

        public void ResetFrame()
        {
            _currentFrame = 0;
        }

        public bool IsFinished()
        {
            return _currentFrame >= _inputList.Count;
        }
    }
}
