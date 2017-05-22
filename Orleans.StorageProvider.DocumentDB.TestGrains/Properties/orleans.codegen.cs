#if !EXCLUDE_CODEGEN
#pragma warning disable 162
#pragma warning disable 219
#pragma warning disable 414
#pragma warning disable 649
#pragma warning disable 693
#pragma warning disable 1591
#pragma warning disable 1998
[assembly: global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0")]
[assembly: global::Orleans.CodeGeneration.OrleansCodeGenerationTargetAttribute("Orleans.StorageProvider.Arango.TestGrains, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
namespace Orleans.StorageProvider.Arango.TestGrains
{
    using global::Orleans.Async;
    using global::Orleans;
    using global::System.Reflection;

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::Orleans.StorageProvider.Arango.TestGrains.IGrain1))]
    internal class OrleansCodeGenGrain1Reference : global::Orleans.Runtime.GrainReference, global::Orleans.StorageProvider.Arango.TestGrains.IGrain1
    {
        protected @OrleansCodeGenGrain1Reference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGrain1Reference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -1233062554;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::Orleans.StorageProvider.Arango.TestGrains.IGrain1";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -1233062554;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -1233062554:
                    switch (@methodId)
                    {
                        case -1629480600:
                            return "Set";
                        case -940922787:
                            return "Get";
                        case -1730113398:
                            return "Clear";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1233062554 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task @Set(global::System.String @stringValue, global::System.Int32 @intValue, global::System.DateTime @dateTimeValue, global::System.Guid @guidValue)
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1629480600, new global::System.Object[]{@stringValue, @intValue, @dateTimeValue, @guidValue});
        }

        public global::System.Threading.Tasks.Task<global::System.Tuple<global::System.String, global::System.Int32, global::System.DateTime, global::System.Guid>> @Get()
        {
            return base.@InvokeMethodAsync<global::System.Tuple<global::System.String, global::System.Int32, global::System.DateTime, global::System.Guid>>(-940922787, null);
        }

        public global::System.Threading.Tasks.Task @Clear()
        {
            return base.@InvokeMethodAsync<global::System.Object>(-1730113398, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::Orleans.StorageProvider.Arango.TestGrains.IGrain1", -1233062554, typeof (global::Orleans.StorageProvider.Arango.TestGrains.IGrain1)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGrain1MethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::Orleans.CodeGeneration.InvokeMethodRequest @request)
        {
            global::System.Int32 interfaceId = @request.@InterfaceId;
            global::System.Int32 methodId = @request.@MethodId;
            global::System.Object[] arguments = @request.@Arguments;
            if (@grain == null)
                throw new global::System.ArgumentNullException("grain");
            switch (interfaceId)
            {
                case -1233062554:
                    switch (methodId)
                    {
                        case -1629480600:
                            return ((global::Orleans.StorageProvider.Arango.TestGrains.IGrain1)@grain).@Set((global::System.String)arguments[0], (global::System.Int32)arguments[1], (global::System.DateTime)arguments[2], (global::System.Guid)arguments[3]).@Box();
                        case -940922787:
                            return ((global::Orleans.StorageProvider.Arango.TestGrains.IGrain1)@grain).@Get().@Box();
                        case -1730113398:
                            return ((global::Orleans.StorageProvider.Arango.TestGrains.IGrain1)@grain).@Clear().@Box();
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1233062554 + ",methodId=" + methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + interfaceId);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -1233062554;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Orleans.StorageProvider.Arango.TestGrains.MyState)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenOrleans_StorageProvider_Arango_TestGrains_MyStateSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Orleans.StorageProvider.Arango.TestGrains.MyState input = ((global::Orleans.StorageProvider.Arango.TestGrains.MyState)original);
            global::Orleans.StorageProvider.Arango.TestGrains.MyState result = new global::Orleans.StorageProvider.Arango.TestGrains.MyState();
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            result.@DateTimeValue = input.@DateTimeValue;
            result.@GuidValue = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@GuidValue);
            result.@IntValue = input.@IntValue;
            result.@StringValue = input.@StringValue;
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Orleans.StorageProvider.Arango.TestGrains.MyState input = (global::Orleans.StorageProvider.Arango.TestGrains.MyState)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@DateTimeValue, stream, typeof (global::System.DateTime));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@GuidValue, stream, typeof (global::System.Guid));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@IntValue, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@StringValue, stream, typeof (global::System.String));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Orleans.StorageProvider.Arango.TestGrains.MyState result = new global::Orleans.StorageProvider.Arango.TestGrains.MyState();
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@DateTimeValue = (global::System.DateTime)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.DateTime), stream);
            result.@GuidValue = (global::System.Guid)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Guid), stream);
            result.@IntValue = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@StringValue = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            return (global::Orleans.StorageProvider.Arango.TestGrains.MyState)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Orleans.StorageProvider.Arango.TestGrains.MyState), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenOrleans_StorageProvider_Arango_TestGrains_MyStateSerializer()
        {
            Register();
        }
    }
}
#pragma warning restore 162
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 649
#pragma warning restore 693
#pragma warning restore 1591
#pragma warning restore 1998
#endif
