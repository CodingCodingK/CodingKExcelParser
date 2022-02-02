﻿#if FEAT_SERVICECONFIGMODEL
using System;
using System.ServiceModel.Configuration;

namespace ProtoBuf.ServiceModel
{
    /// <summary>
    /// Configuration element to swap out DatatContractSerilaizer with the XmlProtoSerializer for a given endpoint.
    /// </summary>
    /// <seealso cref="ProtoEndpointBehavior"/>
    public class ProtoBehaviorExtension : BehaviorExtensionElement
    {
        /// <summary>
        /// Creates a new ProtoBehaviorExtension instance.
        /// </summary>
        public ProtoBehaviorExtension() { }
        /// <summary>
        /// Gets the type of behavior.
        /// </summary>     
        public override Type BehaviorType => typeof(ProtoEndpointBehavior);

        /// <summary>
        /// Creates a behavior extension based on the current configuration settings.
        /// </summary>
        /// <returns>The behavior extension.</returns>
        protected override object CreateBehavior() => new ProtoEndpointBehavior();
    }
}
#endif