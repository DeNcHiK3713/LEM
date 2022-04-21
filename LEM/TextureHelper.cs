using AssetsTools.NET;
using AssetsTools.NET.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEM
{
    public static class TextureHelper
    {
        public static byte[] GetRawTextureBytes(TextureFile texFile, AssetBundleFile bundle)
        {
            texFile.GetTextureData(null, bundle);
            return texFile.pictureData;
        }

        public static void SetRawTextureBytes(TextureFile tex, AssetTypeValueField baseField, byte[] encImageBytes, TextureFormat fmt)
        {
            AssetTypeValueField m_StreamData = baseField.Get("m_StreamData");
            m_StreamData.Get("offset").GetValue().Set(0);
            m_StreamData.Get("size").GetValue().Set(0);
            m_StreamData.Get("path").GetValue().Set("");

            if (!baseField.Get("m_MipCount").IsDummy())
                baseField.Get("m_MipCount").GetValue().Set(1);

            baseField.Get("m_TextureFormat").GetValue().Set((int)fmt);
            baseField.Get("m_CompleteImageSize").GetValue().Set(encImageBytes.Length);

            baseField.Get("m_Width").GetValue().Set(tex.m_Width);
            baseField.Get("m_Height").GetValue().Set(tex.m_Height);

            AssetTypeValueField image_data = baseField.Get("image data");
            image_data.GetValue().type = EnumValueTypes.ByteArray;
            image_data.templateField.valueType = EnumValueTypes.ByteArray;
            AssetTypeByteArray byteArray = new AssetTypeByteArray()
            {
                size = (uint)encImageBytes.Length,
                data = encImageBytes
            };
            image_data.GetValue().Set(byteArray);
        }
    }

}
