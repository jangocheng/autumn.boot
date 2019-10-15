﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Autumn.SignalrChat
{
    public class MessageData
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }
        /// <summary>
        /// 发送用户(0表示系统消息发送用户)
        /// </summary>
        public string SendUserId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageBody { get; set; }
    }
}
