using System.Text.Json;
using UnfoldGeometry.Serialization;

namespace Tests
{
    public class SerializationTest
    {
        [Fact]
        public void BaseCard_serialization()
        {
            var def = new BaseCardDef(new StructureId("testcard"), 148, 210, new AxisDef(AxisDef.AxisTypes.Manual, null));
            var serialized = JsonSerializer.Serialize(def);
            Assert.Equal("""{"Id":{"Id":"testcard"},"Width":148,"Height":210,"Axis":{"Type":"Manual","DependantProperties":null}}""", serialized);
        }

        [Fact]
        public void BaseCard_deserialization()
        {
            var str = """{"Id":{"Id":"testcard"},"Width":148,"Height":210,"Axis":{"Type":"Manual","DependantProperties":null}}""";
            var deserialized = JsonSerializer.Deserialize<BaseCardDef>(str);
            var def = new BaseCardDef(new StructureId("testcard"), 148, 210, new AxisDef(AxisDef.AxisTypes.Manual, null));
            Assert.Equal(def, deserialized);
        }

        [Fact]
        public void Collection_round_trip()
        {
            var coll = new StructureDefCollection(new List<IStructureDef> {
                new BaseCardDef(new StructureId("test1"), 148, 210, new AxisDef(AxisDef.AxisTypes.Manual, null)),
                new SymmetricParallelogramDef(new StructureId("test2"), new AxisDef(AxisDef.AxisTypes.Manual, null), 10, 50, 20)
            });

            var str = JsonSerializer.Serialize(coll);

            var deserialized = JsonSerializer.Deserialize<StructureDefCollection>(str);

            Assert.NotNull(deserialized);
            Assert.Equal(coll.Children.Count, deserialized.Children.Count);
            Assert.All(deserialized.Children, (x) => { Assert.Equal(coll.Children[x.Key], x.Value); });
        }
    }
}
