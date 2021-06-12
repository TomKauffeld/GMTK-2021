public static class Settings
{
    public static Layout Layout = Layout.QWERTY;

    public static bool DebugView = false;


    public static float Sensibility = 1;
    public static bool InverseYaw = false;
    public static bool InversePitch = false;



    public static void SwitchLayout() => Layout = Layout == Layout.AZERTY ? Layout.QWERTY : Layout.AZERTY;
    public static void SwitchDebugView() => DebugView = !DebugView;
}