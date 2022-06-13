using appWPF;
using appWPF.Conection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestProject.TestesUnitarios
{
    public class TestesUnitarios
    {
        private Pokemon pokemon;
        private ConnectionMysql conn;

        public TestesUnitarios()
        {
            pokemon = new Pokemon(new Mock<Pokemon>(1, "Raichu", "Eletrico", "Domynik").Object);
            conn = new ConnectionMysql(new Mock<ConnectionMysql>().Object, new Mock<IConnection>().Object);
        }

        [Fact]
        public void Test_Add_Func()
        {
            Assert.Throws<Exception>(() => !conn.TestConnection());
        }
    }
}
