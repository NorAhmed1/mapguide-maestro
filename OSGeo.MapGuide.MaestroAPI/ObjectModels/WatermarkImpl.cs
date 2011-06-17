﻿#region Disclaimer / License
// Copyright (C) 2011, Jackie Ng
// http://trac.osgeo.org/mapguide/wiki/maestro, jumpinjackie@gmail.com
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using OSGeo.MapGuide.ObjectModels.WatermarkDefinition;
using System.Xml.Serialization;
using OSGeo.MapGuide.MaestroAPI;
using OSGeo.MapGuide.MaestroAPI.Resource;

namespace OSGeo.MapGuide.ObjectModels.WatermarkDefinition_2_3_0
{
    partial class WatermarkDefinition : IWatermarkDefinition
    {
        private static readonly Version RES_VERSION = new Version(2, 3, 0);

        [XmlIgnore]
        public ResourceTypes ResourceType
        {
            get
            {
                return ResourceTypes.WatermarkDefinition;
            }
        }

        [XmlIgnore]
        public Version ResourceVersion
        {
            get
            {
                return RES_VERSION;
            }
        }

        [XmlAttribute("noNamespaceSchemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string ValidatingSchema
        {
            get { return "WatermarkDefinition-2.3.0.xsd"; }
            set { }
        }

        [XmlIgnore]
        public bool IsStronglyTyped
        {
            get { return true; }
        }

        [XmlIgnore]
        object IWatermarkDefinition.Content
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [XmlIgnore]
        IWatermarkAppearance IWatermarkDefinition.Appearance
        {
            get
            {
                return this.Appearance;
            }
            set
            {
                this.Appearance = (WatermarkAppearanceType)value;
            }
        }

        [XmlIgnore]
        IPosition IWatermarkDefinition.Position
        {
            get
            {
                if (this.Position != null)
                    return this.Position.Item;
                return null;
            }
            set
            {
                if (value != null)
                {
                    if (this.Position == null)
                        this.Position = new WatermarkDefinitionTypePosition();

                    this.Position.Item = (PositionType)value;
                }
            }
        }

        [XmlIgnore]
        OSGeo.MapGuide.MaestroAPI.IServerConnection OSGeo.MapGuide.MaestroAPI.Resource.IResource.CurrentConnection
        {
            get;
            set;
        }

        private string _resId;

        [XmlIgnore]
        public string ResourceID
        {
            get
            {
                return _resId;
            }
            set
            {
                if (!ResourceIdentifier.Validate(value))
                    throw new InvalidOperationException("Not a valid resource identifier"); //LOCALIZE

                var res = new ResourceIdentifier(value);
                if (res.Extension != ResourceTypes.WatermarkDefinition.ToString())
                    throw new InvalidOperationException("Invalid resource identifier for this type of object: " + res.Extension); //LOCALIZE

                _resId = value;
                this.OnPropertyChanged("ResourceID");
            }
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }

    partial class WatermarkType : IWatermark
    {
        [XmlIgnore]
        IWatermarkAppearance IWatermark.AppearanceOverride
        {
            get
            {
                return this.AppearanceOverride;
            }
            set
            {
                this.AppearanceOverride = (WatermarkAppearanceType)value;
            }
        }

        [XmlIgnore]
        IPosition IWatermark.PositionOverride
        {
            get
            {
                return this.PositionOverride.Item;
            }
            set
            {
                this.PositionOverride.Item = (PositionType)value;
            }
        }
    }

    partial class WatermarkAppearanceType : IWatermarkAppearance { }

    partial class WatermarkTypePositionOverride { }

    partial class XYPositionType : IXYPosition 
    {
        [XmlIgnore]
        public override OSGeo.MapGuide.ObjectModels.WatermarkDefinition.PositionType Type
        {
            get { return OSGeo.MapGuide.ObjectModels.WatermarkDefinition.PositionType.XY; }
        }

        [XmlIgnore]
        IHorizontalPosition IXYPosition.XPosition
        {
            get
            {
                return this.XPosition;
            }
            set
            {
                this.XPosition = (HorizontalPositionType)value;
            }
        }

        [XmlIgnore]
        IVerticalPosition IXYPosition.YPosition
        {
            get
            {
                return this.YPosition;
            }
            set
            {
                this.YPosition = (VerticalPositionType)value;
            }
        }
    }

    abstract partial class PositionType : IPosition
    {
        [XmlIgnore]
        public abstract OSGeo.MapGuide.ObjectModels.WatermarkDefinition.PositionType Type { get; }
    }

    partial class TilePositionType : ITilePosition
    {
        [XmlIgnore]
        public override OSGeo.MapGuide.ObjectModels.WatermarkDefinition.PositionType Type
        {
            get { return OSGeo.MapGuide.ObjectModels.WatermarkDefinition.PositionType.Tile; ; }
        }

        [XmlIgnore]
        IHorizontalPosition ITilePosition.HorizontalPosition
        {
            get
            {
                return this.HorizontalPosition;
            }
            set
            {
                this.HorizontalPosition = (HorizontalPositionType)value;
            }
        }

        [XmlIgnore]
        IVerticalPosition ITilePosition.VerticalPosition
        {
            get
            {
                return this.VerticalPosition;
            }
            set
            {
                this.VerticalPosition = (VerticalPositionType)value;
            }
        }
    }

    partial class VerticalPositionType : IVerticalPosition { }

    partial class HorizontalPositionType : IHorizontalPosition { }
}