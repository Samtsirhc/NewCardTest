public enum E_EventType
{
    // examples
    TEST_TYPE = 1,

    // 抽牌打牌相关
        DRAW_CARD = 10000,
        DELETE_CARD = 10001,
        NEW_TURN_START = 10002,
        CARD_USED = 10003,
        PLAY_ONE_CARD = 10004,
        PLAY_ONE_CARD_IN_TURN = 10005,
        SWITCH_CARD = 10006,

        // 设置属性
        SET_HP = 20000,
        SET_ARMOR = 20001,
        SET_ANGER = 20002,
        SET_CALM = 20003,


        // 回合相关
        END_TURN = 30000,

        // UI相关
        SHOW_ARROW = 40000,
        UI_POINTER_ENTER = 40001,
        UI_POINTER_OUT = 40002,



}