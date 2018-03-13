using System.Collections.ObjectModel;

namespace SF.Framework.Layout.Header.Ribbon
{
    public class FanLayoutRibbonPage
    {
        public string Title;

        public Collection<FanLayoutRibbonPageGroup> PageGroups = new Collection<FanLayoutRibbonPageGroup>();

    }
}
