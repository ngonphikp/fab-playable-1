/**
 * IDynamicScrollViewItem.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

namespace Mosframe {

    /// <summary>
    /// DynamicScrollView Item interface
    /// </summary>
    public interface IDynamicScrollViewItem {
        int getIndex();
        
        void onUpdateItem( int index );
    }
}
