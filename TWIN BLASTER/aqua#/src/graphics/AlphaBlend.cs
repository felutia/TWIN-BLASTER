
/*!
 *  @file       AlphaBlend.cs
 *  @brief      アルファブレンド
 *  @author     Kazuya Maruyama
 *  @date       2014/01/02
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

namespace aqua
{
    /// <summary>
    /// アルファブレンドタイプ
    /// </summary>
    public enum ALPHABLEND_TYPE
    {
          NONE      = 0 // ブレンドなし
        , TRANS         // 半透明
        , ADD           // 加算
        , TRANSADD      // 半加算
        , SUB           // 減算
        , MUL           // 乗算
        , REVERSE       // 反転
    }
}
