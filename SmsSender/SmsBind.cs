using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmsSender
{
    class SmsBind
    {
        private long id;
        private String uuid;
        private String bindphone;
        private String sn;
        private String content;
        private int type;
        private String sendphone;
        private String receivephone;
        private DateTime createtime;
        private String name;
        private String userKey;
        private String address;
        private String idnumber;
        private String state;

        public long getId()
        {
            return id;
        }

        public DateTime getCreatetime()
        {
            return createtime;
        }

        public void setCreatetime(DateTime createtime)
        {
            this.createtime = createtime;
        }

        public void setId(long id)
        {
            this.id = id;
        }

        public String getUuid()
        {
            return uuid;
        }

        public void setUuid(String uuid)
        {
            this.uuid = uuid;
        }

        public String getBindphone()
        {
            return bindphone;
        }

        public void setBindphone(String bindphone)
        {
            this.bindphone = bindphone;
        }

        public String getSn()
        {
            return sn;
        }

        public void setSn(String sn)
        {
            this.sn = sn;
        }

        public String getContent()
        {
            return content;
        }

        public void setContent(String content)
        {
            this.content = content;
        }

        public int getType()
        {
            return type;
        }

        public void setType(int type)
        {
            this.type = type;
        }

        public String getSendphone()
        {
            return sendphone;
        }

        public void setSendphone(String sendphone)
        {
            this.sendphone = sendphone;
        }

        public String getReceivephone()
        {
            return receivephone;
        }

        public void setReceivephone(String receivephone)
        {
            this.receivephone = receivephone;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public String getUserKey()
        {
            return userKey;
        }

        public void setUserKey(String userKey)
        {
            this.userKey = userKey;
        }

        public String getAddress()
        {
            return address;
        }

        public void setAddress(String address)
        {
            this.address = address;
        }

        public String getIdnumber()
        {
            return idnumber;
        }

        public void setIdnumber(String idnumber)
        {
            this.idnumber = idnumber;
        }

        public String getState()
        {
            return state;
        }

        public void setState(String state)
        {
            this.state = state;
        }
    }
}
