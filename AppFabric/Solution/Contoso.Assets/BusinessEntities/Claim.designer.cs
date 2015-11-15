﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.21006.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessEntities
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
    using System.Runtime.Serialization;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="ContosoDB")]
	public partial class ClaimDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertClaim(Claim instance);
    partial void UpdateClaim(Claim instance);
    partial void DeleteClaim(Claim instance);
    partial void InsertAccident(Accident instance);
    partial void UpdateAccident(Accident instance);
    partial void DeleteAccident(Accident instance);
    #endregion
		
		public ClaimDataContext() : 
				base(global::BusinessEntities.Properties.Settings.Default.ContosoDBConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ClaimDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ClaimDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ClaimDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ClaimDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Claim> Claims
		{
			get
			{
				return this.GetTable<Claim>();
			}
		}
		
		public System.Data.Linq.Table<Accident> Accidents
		{
			get
			{
				return this.GetTable<Accident>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Claims")]
    [DataContract]
	public partial class Claim : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ClaimId;
		
		private int _AccidentId;
		
		private string _Description;
		
		private string _Status;
		
		private System.Nullable<System.DateTime> _DateCreated;
		
		private string _RentalCar;
		
		private EntityRef<Accident> _Accidents;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnClaimIdChanging(int value);
    partial void OnClaimIdChanged();
    partial void OnAccidentIdChanging(int value);
    partial void OnAccidentIdChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnStatusChanging(string value);
    partial void OnStatusChanged();
    partial void OnDateCreatedChanging(System.Nullable<System.DateTime> value);
    partial void OnDateCreatedChanged();
    partial void OnRentalCarChanging(string value);
    partial void OnRentalCarChanged();
    #endregion
		
		public Claim()
		{
			this._Accidents = default(EntityRef<Accident>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClaimId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		[DataMember]
        public int ClaimId
		{
			get
			{
				return this._ClaimId;
			}
			set
			{
				if ((this._ClaimId != value))
				{
					this.OnClaimIdChanging(value);
					this.SendPropertyChanging();
					this._ClaimId = value;
					this.SendPropertyChanged("ClaimId");
					this.OnClaimIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccidentId", DbType="Int NOT NULL")]
        [DataMember]
        public int AccidentId
		{
			get
			{
				return this._AccidentId;
			}
			set
			{
				if ((this._AccidentId != value))
				{
					this.OnAccidentIdChanging(value);
					this.SendPropertyChanging();
					this._AccidentId = value;
					this.SendPropertyChanged("AccidentId");
					this.OnAccidentIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(255)")]
        [DataMember]
        public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="NVarChar(50)")]
        [DataMember]
        public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateCreated", DbType="DateTime")]
        [DataMember]
        public System.Nullable<System.DateTime> DateCreated
		{
			get
			{
				return this._DateCreated;
			}
			set
			{
				if ((this._DateCreated != value))
				{
					this.OnDateCreatedChanging(value);
					this.SendPropertyChanging();
					this._DateCreated = value;
					this.SendPropertyChanged("DateCreated");
					this.OnDateCreatedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RentalCar", DbType="NVarChar(50)")]
        [DataMember]
        public string RentalCar
		{
			get
			{
				return this._RentalCar;
			}
			set
			{
				if ((this._RentalCar != value))
				{
					this.OnRentalCarChanging(value);
					this.SendPropertyChanging();
					this._RentalCar = value;
					this.SendPropertyChanged("RentalCar");
					this.OnRentalCarChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Claim_Accident", Storage="_Accidents", ThisKey="AccidentId", OtherKey="AccidentId", IsUnique=true, IsForeignKey=false)]
        [DataMember]
        public Accident Accidents
		{
			get
			{
				return this._Accidents.Entity;
			}
			set
			{
				Accident previousValue = this._Accidents.Entity;
				if (((previousValue != value) 
							|| (this._Accidents.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Accidents.Entity = null;
						previousValue.Claim = null;
					}
					this._Accidents.Entity = value;
					if ((value != null))
					{
						value.Claim = this;
					}
					this.SendPropertyChanged("Accidents");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Accidents")]
	[DataContract]
    public partial class Accident : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _AccidentId;
		
		private string _FName;
		
		private string _LName;
		
		private string _Address;
		
		private string _ContactPhone;
		
		private string _City;
		
		private string _Zip;
		
		private string _State;
		
		private System.Nullable<double> _Latitude;
		
		private System.Nullable<double> _Longitude;
		
		private EntityRef<Claim> _Claim;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnAccidentIdChanging(int value);
    partial void OnAccidentIdChanged();
    partial void OnFNameChanging(string value);
    partial void OnFNameChanged();
    partial void OnLNameChanging(string value);
    partial void OnLNameChanged();
    partial void OnAddressChanging(string value);
    partial void OnAddressChanged();
    partial void OnContactPhoneChanging(string value);
    partial void OnContactPhoneChanged();
    partial void OnCityChanging(string value);
    partial void OnCityChanged();
    partial void OnZipChanging(string value);
    partial void OnZipChanged();
    partial void OnStateChanging(string value);
    partial void OnStateChanged();
    partial void OnLatitudeChanging(System.Nullable<double> value);
    partial void OnLatitudeChanged();
    partial void OnLongitudeChanging(System.Nullable<double> value);
    partial void OnLongitudeChanged();
    #endregion
		
		public Accident()
		{
			this._Claim = default(EntityRef<Claim>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AccidentId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
        [DataMember]
        public int AccidentId
		{
			get
			{
				return this._AccidentId;
			}
			set
			{
				if ((this._AccidentId != value))
				{
					if (this._Claim.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAccidentIdChanging(value);
					this.SendPropertyChanging();
					this._AccidentId = value;
					this.SendPropertyChanged("AccidentId");
					this.OnAccidentIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FName", DbType="NVarChar(50)")]
        [DataMember]
        public string FName
		{
			get
			{
				return this._FName;
			}
			set
			{
				if ((this._FName != value))
				{
					this.OnFNameChanging(value);
					this.SendPropertyChanging();
					this._FName = value;
					this.SendPropertyChanged("FName");
					this.OnFNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LName", DbType="NVarChar(50)")]
        [DataMember]
        public string LName
		{
			get
			{
				return this._LName;
			}
			set
			{
				if ((this._LName != value))
				{
					this.OnLNameChanging(value);
					this.SendPropertyChanging();
					this._LName = value;
					this.SendPropertyChanged("LName");
					this.OnLNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Address", DbType="NVarChar(50)")]
        [DataMember]
        public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if ((this._Address != value))
				{
					this.OnAddressChanging(value);
					this.SendPropertyChanging();
					this._Address = value;
					this.SendPropertyChanged("Address");
					this.OnAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContactPhone", DbType="NVarChar(20)")]
        [DataMember]
        public string ContactPhone
		{
			get
			{
				return this._ContactPhone;
			}
			set
			{
				if ((this._ContactPhone != value))
				{
					this.OnContactPhoneChanging(value);
					this.SendPropertyChanging();
					this._ContactPhone = value;
					this.SendPropertyChanged("ContactPhone");
					this.OnContactPhoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_City", DbType="NVarChar(50)")]
        [DataMember]
        public string City
		{
			get
			{
				return this._City;
			}
			set
			{
				if ((this._City != value))
				{
					this.OnCityChanging(value);
					this.SendPropertyChanging();
					this._City = value;
					this.SendPropertyChanged("City");
					this.OnCityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Zip", DbType="NChar(5)")]
        [DataMember]
        public string Zip
		{
			get
			{
				return this._Zip;
			}
			set
			{
				if ((this._Zip != value))
				{
					this.OnZipChanging(value);
					this.SendPropertyChanging();
					this._Zip = value;
					this.SendPropertyChanged("Zip");
					this.OnZipChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_State", DbType="NChar(2)")]
        [DataMember]
        public string State
		{
			get
			{
				return this._State;
			}
			set
			{
				if ((this._State != value))
				{
					this.OnStateChanging(value);
					this.SendPropertyChanging();
					this._State = value;
					this.SendPropertyChanged("State");
					this.OnStateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Latitude", DbType="Float")]
        [DataMember]
        public System.Nullable<double> Latitude
		{
			get
			{
				return this._Latitude;
			}
			set
			{
				if ((this._Latitude != value))
				{
					this.OnLatitudeChanging(value);
					this.SendPropertyChanging();
					this._Latitude = value;
					this.SendPropertyChanged("Latitude");
					this.OnLatitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Longitude", DbType="Float")]
        [DataMember]
        public System.Nullable<double> Longitude
		{
			get
			{
				return this._Longitude;
			}
			set
			{
				if ((this._Longitude != value))
				{
					this.OnLongitudeChanging(value);
					this.SendPropertyChanging();
					this._Longitude = value;
					this.SendPropertyChanged("Longitude");
					this.OnLongitudeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Claim_Accident", Storage="_Claim", ThisKey="AccidentId", OtherKey="AccidentId", IsForeignKey=true)]
		public Claim Claim
		{
			get
			{
				return this._Claim.Entity;
			}
			set
			{
				Claim previousValue = this._Claim.Entity;
				if (((previousValue != value) 
							|| (this._Claim.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Claim.Entity = null;
						previousValue.Accidents = null;
					}
					this._Claim.Entity = value;
					if ((value != null))
					{
						value.Accidents = this;
						this._AccidentId = value.AccidentId;
					}
					else
					{
						this._AccidentId = default(int);
					}
					this.SendPropertyChanged("Claim");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591