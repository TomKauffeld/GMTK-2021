public static class Settings
{
    public static Layout Layout = Layout.QWERTY;

    public static bool DebugView = false;




    public static void SwitchLayout() => Layout = Layout == Layout.AZERTY ? Layout.QWERTY : Layout.AZERTY;
    public static void SwitchDebugView() => DebugView = !DebugView;
}