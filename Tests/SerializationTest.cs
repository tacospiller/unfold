using System.Text.Json;
using Unfold.Serialization;

namespace Tests
{
    public class SerializationTest
    {
        [Fact]
        public void BaseCard_serialization()
        {
            var def = new BaseCardDef { Id = new StructureId("testcard"), Width = 148, Height = 210, Axis = new AxisDef { Type = AxisDef.AxisTypes.Manual } };
            var serialized = JsonSerializer.Serialize(def);
            Assert.Equal("""{"Id":{"Id":"testcard"},"Width":148,"Height":210,"Axis":{"Type":"Manual","DependantProperties":null}}""", serialized);
        }

        [Fact]
        public void BaseCard_deserialization()
        {
            var str = """{"Id":{"Id":"testcard"},"Width":148,"Height":210,"Axis":{"Type":"Manual","DependantProperties":null}}""";
            var deserialized = JsonSerializer.Deserialize<BaseCardDef>(str);
            var def = new BaseCardDef{ Id = new StructureId("testcard"), Width = 148, Height = 210, Axis = new AxisDef{ Type = AxisDef.AxisTypes.Manual } };
            Assert.Equal(def, deserialized);
        }

        [Fact]
        public void Collection_round_trip()
        {
            var coll = new List<IStructureDef> {
                new BaseCardDef{ Id = new StructureId("test1"), Width = 148, Height = 210, Axis = new AxisDef{ Type = AxisDef.AxisTypes.Manual } },
                new SymmetricParallelogramDef{ Id = new StructureId("test2"), Axis = new AxisDef{ Type = AxisDef.AxisTypes.Manual }, DistFromAxis = 10, Width = 50, Height = 20 }
            };

            var str = JsonSerializer.Serialize(coll);

            var deserialized = JsonSerializer.Deserialize<List<IStructureDef>>(str);

            Assert.NotNull(deserialized);
            Assert.Equal(coll.Count, deserialized.Count);
            Assert.All(deserialized, (x, i) => { Assert.Equal(coll[i], x); });
        }
    }
}
