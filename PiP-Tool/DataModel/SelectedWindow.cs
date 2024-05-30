﻿using PiP_Tool.Native;
using PiP_Tool.Shared.Helpers;

namespace PiP_Tool.DataModel
{
    public class SelectedWindow
    {

        #region public

        public WindowInfo WindowInfo { get; set; }
        public NativeStructs.Rect SelectedRegion { get; set; }

        public NativeStructs.Rect SelectedRegionNoBorder => new NativeStructs.Rect(
            SelectedRegion.Left - WindowInfo.Border.Left,
            SelectedRegion.Top - WindowInfo.Border.Top,
            SelectedRegion.Right - WindowInfo.Border.Left,
            SelectedRegion.Bottom - WindowInfo.Border.Top
        );
        /// <summary>
        /// Gets ratio width / height
        /// </summary>
        public float Ratio => WindowInfo.Size.Height > 0 ? SelectedRegion.Width / (float)SelectedRegion.Height : 0;
        /// <summary>
        /// Gets ratio height / width
        /// </summary>
        public float RatioHeightByWidth => WindowInfo.Size.Width > 0 ? SelectedRegion.Height / (float)SelectedRegion.Width : 0;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="windowInfo">Selected window</param>
        /// <param name="selectedRegion">Selected region</param>
        public SelectedWindow(WindowInfo windowInfo, NativeStructs.Rect selectedRegion)
        {
            WindowInfo = windowInfo;

            selectedRegion = new NativeStructs.Rect(
                (int)(selectedRegion.Left * windowInfo.DpiX),
                (int)(selectedRegion.Top * windowInfo.DpiY),
                (int)(selectedRegion.Right * windowInfo.DpiX),
                (int)(selectedRegion.Bottom * windowInfo.DpiY)
            );

            SelectedRegion = new NativeStructs.Rect(
                selectedRegion.Left + WindowInfo.Border.Left,
                selectedRegion.Top + WindowInfo.Border.Top,
                selectedRegion.Right + WindowInfo.Border.Left,
                selectedRegion.Bottom + WindowInfo.Border.Top
                );
        }

    }
}
