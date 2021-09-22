using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class FileItem
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 概念としてのファイルアイテム名
        /// 「2020年度分配金のお知らせ」
        /// などが入るイメージ
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 物理的なファイル名を保持する。
        /// 本来はFileItemVersionオブジェクト/テーブルに保持するべきもの
        /// 
        /// 拡張子などを保持するためにも必要
        /// ダウンロード時のデフォルトのファイル名として使われる
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// アップロードされたファイル
        /// 本来はFileItemVersionオブジェクト/テーブルに保持するべきもの
        /// 
        /// Cloudのみを前提にするのであれば、FileはAzure Blob Storageに追い出したほうが 
        /// コスト的には有利だと考えています
        /// </summary>
        public byte[] File { get; set; }

        /// <summary>
        /// 楽観的排他制御のためのタイムスタンプ
        /// </summary>
        [Timestamp]
        public byte[] Version { get; set; }
    }
}
