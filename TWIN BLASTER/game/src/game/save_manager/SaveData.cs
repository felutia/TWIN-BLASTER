
/*!
 *  @file       SaveData.cs
 *  @brief      セーブデータ
 *  @author     Kazuya Maruyama
 *  @date       2014/10/09
 *  @since      1.0
 *
 *  Copyright (c) 2013 2014, Kazuya Maruyama. All rights reserved.
 */

using System;

namespace game
{
    /// <summary>
    /// セーブデータクラス
    /// </summary>
    [Serializable()]
    public class CSaveData
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CSaveData( )
        {
			first=false;
        }
		
		public bool first;
    }
}
