/// <summary>
/// Контракт для приведения объекта к интерфейсу при сериализации в json
/// </summary>
/// <remarks>
/// Сериализуются в json только те свойства объекта, которые содержатся в реализуемом интерфейсе
/// </remarks>
public class InterfaceContractResolver : DefaultContractResolver
{
	private readonly Type _interfaceType;

	public InterfaceContractResolver(Type InterfaceType) : base()
	{
		_interfaceType = InterfaceType;
		NamingStrategy = new CamelCaseNamingStrategy
		{
			ProcessDictionaryKeys = true,
			OverrideSpecifiedNames = true
		};
	}

	protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
	{
		var properties = type.GetInterface(_interfaceType.Name) == _interfaceType
					? base.CreateProperties(_interfaceType, memberSerialization)
					: base.CreateProperties(type, memberSerialization);

		return properties;
	}
}
