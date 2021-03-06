using System.ComponentModel;

namespace Util.Ui.FlexLayout.Enums {
    /// <summary>
    /// Y轴垂直对齐方式
    /// </summary>
    public enum YAlign {
        /// <summary>
        /// 起始对齐
        /// </summary>
        [Description( "start" )]
        Start,
        /// <summary>
        /// 居中对齐
        /// </summary>
        [Description( "center" )]
        Center,
        /// <summary>
        /// 末尾对齐
        /// </summary>
        [Description( "end" )]
        End,
        /// <summary>
        /// 每个元素两侧的间隔相等
        /// </summary>
        [Description( "space-around" )]
        SpaceAround,
        /// <summary>
        /// 两端对齐，元素之间的间隔相等
        /// </summary>
        [Description( "space-between" )]
        SpaceBetween,
        /// <summary>
        /// space-evenly
        /// </summary>
        [Description( "space-evenly" )]
        SpaceEvenly,
        /// <summary>
        /// 拉伸
        /// </summary>
        [Description( "stretch" )]
        Stretch
    }
}