using System.Windows;

namespace SniffCore.MessageBoxes
{
    public sealed class MessageBoxOptions : IMessageBoxOptions
    {
        public MessageBoxOptions(MessageBoxImage? icon)
            : this(icon, null)
        {
        }

        public MessageBoxOptions(MessageBoxResult? defaultResult)
            : this(null, defaultResult)
        {
        }

        public MessageBoxOptions(MessageBoxImage? icon, MessageBoxResult? defaultResult)
        {
            Icon = icon;
            DefaultResult = defaultResult;
        }

        public MessageBoxImage? Icon { get; set; }
        public MessageBoxResult? DefaultResult { get; set; }
    }
}