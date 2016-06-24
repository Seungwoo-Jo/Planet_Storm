using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class GamePadInput {

    private static GamePadInput instance;

    private PlayerIndex m_PlayerIndex;
    private GamePadState m_PadState;

    public struct Controller
    {
        public Vector2 m_LStick;
        public Vector2 m_RStick;
        public Vector2 m_Triggers;
        public Vector2 m_Cross;

        public Vector4 m_Timer;

        public bool[] m_Buttons;
    }

    public Controller[] m_Controller;

    public static GamePadInput Instance
    {
        get
        {
            if(instance == null) {
                instance = new GamePadInput();
                instance.Init();
            }
            return instance;
        }
    }

    public void Init()
    {
        m_Controller = new Controller[4];

        for(int i = 0; i < m_Controller.Length; i++) {
            m_Controller[i].m_Buttons = new bool[8];
        }
    }

    public Vector2 GetStick(int playerIndex, string name)
    {
        if(CheckPlayerIndex(playerIndex)) {
            m_PadState = GamePad.GetState((PlayerIndex)playerIndex);
        }

        if(name.Equals("left")) {
            m_Controller[playerIndex].m_LStick.Set(m_PadState.ThumbSticks.Left.X, m_PadState.ThumbSticks.Left.Y);
            return m_Controller[playerIndex].m_LStick;
        }
        else if(name.Equals("right")) {
            m_Controller[playerIndex].m_RStick.Set(m_PadState.ThumbSticks.Right.X, m_PadState.ThumbSticks.Right.Y);
            return m_Controller[playerIndex].m_RStick;
        }

        return Vector2.zero;
    }

    public Vector2 GetTrigger(int playerIndex)
    {
        m_PadState = GamePad.GetState((PlayerIndex)playerIndex);

        m_Controller[playerIndex].m_Triggers.Set(m_PadState.Triggers.Left, m_PadState.Triggers.Right);
        return m_Controller[playerIndex].m_Triggers;
    }

    // DPad
    public Vector2 GetCross(int playerIndex, bool isButton)
    {
        m_PadState = GamePad.GetState((PlayerIndex)playerIndex);
        Vector2 _result = Vector2.zero;

        if(m_PadState.DPad.Up == ButtonState.Pressed) {
            m_Controller[playerIndex].m_Cross.y = 1.0f;
        }
        else if(m_PadState.DPad.Down == ButtonState.Pressed) {
            m_Controller[playerIndex].m_Cross.y = -1.0f;
        }
        else {
            m_Controller[playerIndex].m_Cross.y = 0.0f;
        }

        if(m_PadState.DPad.Left == ButtonState.Pressed) {
            m_Controller[playerIndex].m_Cross.x = -1.0f;
        }
        else if(m_PadState.DPad.Right == ButtonState.Pressed) {
            m_Controller[playerIndex].m_Cross.x = 1.0f;
        }
        else {
            m_Controller[playerIndex].m_Cross.x = 0.0f;
        }

        m_Controller[playerIndex].m_Cross = (m_Controller[playerIndex].m_Cross - Vector2.zero).normalized;
        _result = m_Controller[playerIndex].m_Cross;

        return _result;
    }

    public bool ButtonDown(int playerIndex, string buttonName)
    {
        if(CheckPlayerIndex(playerIndex)) {
            m_PadState = GamePad.GetState((PlayerIndex)playerIndex);
        }

        ButtonState Button;
        int ButtonIndex = -1;

        Button = NameToButton(buttonName, ref ButtonIndex);
        
        if(Button == ButtonState.Pressed) {
            if(m_Controller[playerIndex].m_Buttons[ButtonIndex] == false) {
                m_Controller[playerIndex].m_Buttons[ButtonIndex] = true;
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }

    public bool ButtonPressed(int playerIndex, string buttonName)
    {
        if(CheckPlayerIndex(playerIndex)) {
            m_PadState = GamePad.GetState((PlayerIndex)playerIndex);
        }
        ButtonState Button;
        int ButtonIndex = -1;

        Button = NameToButton(buttonName, ref ButtonIndex);

        if(Button == ButtonState.Pressed) {
            if(m_Controller[playerIndex].m_Buttons[ButtonIndex] == true) {
                return true;
            }
            else {
                m_Controller[playerIndex].m_Buttons[ButtonIndex] = true;
                return false;
            }
        }
        else {
            return false;
        }
    }

    public bool ButtonUp(int playerIndex, string buttonName)
    {
        if(CheckPlayerIndex(playerIndex)) {
            m_PadState = GamePad.GetState((PlayerIndex)playerIndex);
        }

        ButtonState Button;
        int ButtonIndex = -1;

        Button = NameToButton(buttonName, ref ButtonIndex);
        
        if(Button == ButtonState.Released) {
            if(m_Controller[playerIndex].m_Buttons[ButtonIndex] == true) {
                m_Controller[playerIndex].m_Buttons[ButtonIndex] = false;
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }

    bool CheckPlayerIndex(int index)
    {
        if(index >= 0 && index <= 3) {
            return true;
        }
        else {
            Debug.LogError("Wrong PlayerIndex");
            return false;
        }
    }

    ButtonState NameToButton(string buttonName, ref int buttonIndex)
    {
        ButtonState Button;

        if(buttonName.Equals("L1")) {
            Button = m_PadState.Buttons.LeftShoulder;
            buttonIndex = 0;
        }
        else if(buttonName.Equals("R1")) {
            Button = m_PadState.Buttons.RightShoulder;
            buttonIndex = 1;
        }
        else if(buttonName.Equals("A")) {
            Button = m_PadState.Buttons.A;
            buttonIndex = 2;
        }
        else if(buttonName.Equals("B")) {
            Button = m_PadState.Buttons.B;
            buttonIndex = 3;
        }
        else if(buttonName.Equals("X")) {
            Button = m_PadState.Buttons.X;
            buttonIndex = 4;
        }
        else if(buttonName.Equals("Y")) {
            Button = m_PadState.Buttons.Y;
            buttonIndex = 5;
        }
        else if(buttonName.Equals("L3")) {
            Button = m_PadState.Buttons.LeftStick;
            buttonIndex = 6;
        }
        else if(buttonName.Equals("R3")) {
            Button = m_PadState.Buttons.RightStick;
            buttonIndex = 7;
        }
        else {
            Button = m_PadState.Buttons.X;
            Debug.LogError("Wrong Button Name");
        }

        return Button;
    }
}
