using System.Activities;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace RPA.UiPath.Classlib.Activities
{
    public class MouseEventHorizontal : CodeActivity
    {
        private const uint MOUSEEVENTF_HWHEEL = 0x01000;

        [Category("Input")]
        [RequiredArgument]
        public InArgument<Enum.MouseEventHorizontal> Direction { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<int> Amount { get; set; }

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, uint dwExtraInfo);

        protected override void Execute(CodeActivityContext context)
        {
            var direction = Direction.Get(context);
            var amount = Amount.Get(context);
            var dwData = direction == Enum.MouseEventHorizontal.Right ? amount : -amount;

            mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, dwData, 0);
        }
    }
}
