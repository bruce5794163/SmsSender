using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmsSender
{
public class SmsBindTemp  {

	/**
	 * 
	 */
	
	private long id;
	private String uuid;
	private String sn;
	private String name;
	private String userId;
	private String phone;
	private String address;
	private String idNumber;
	private int receiveFlag;
	private int counter;
	private String userMessageId;
	private String hostName;
	private DateTime createTime;
	public long getId() {
		return id;
	}
	public void setId(long id) {
		this.id = id;
	}
	public String getSn() {
		return sn;
	}
	public void setSn(String sn) {
		this.sn = sn;
	}
	public String getUuid() {
		return uuid;
	}
	public void setUuid(String uuid) {
		this.uuid = uuid;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	
	public String getUserId() {
		return userId;
	}
	public void setUserId(String userId) {
		this.userId = userId;
	}
	public String getPhone() {
		return phone;
	}
	public void setPhone(String phone) {
		this.phone = phone;
	}
	public String getAddress() {
		return address;
	}
	public void setAddress(String address) {
		this.address = address;
	}
	public String getIdNumber() {
		return idNumber;
	}
	public void setIdNumber(String idNumber) {
		this.idNumber = idNumber;
	}
	public int getReceiveFlag() {
		return receiveFlag;
	}
	public void setReceiveFlag(int receiveFlag) {
		this.receiveFlag = receiveFlag;
	}
	public int getCounter() {
		return counter;
	}
	public void setCounter(int counter) {
		this.counter = counter;
	}
	
	public String getUserMessageId() {
		return userMessageId;
	}
	public void setUserMessageId(String userMessageId) {
		this.userMessageId = userMessageId;
	}
    public DateTime getCreateTime()
    {
		return createTime;
	}
    public void setCreateTime(DateTime createTime)
    {
		this.createTime = createTime;
	}
	public String getHostName() {
		return hostName;
	}
	public void setHostName(String hostName) {
		this.hostName = hostName;
	}
}
}
