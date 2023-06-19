using NUnit.Framework;

namespace Tests
{
    public class PlayerMechanicsTests
    {
        [Test]
        public void AddItemToInventory_ItemFits_ReturnsTrue()
        {
            // Arrange
            PlayerMechanics playerMechanics = new PlayerMechanics();
            Item item = new Item();
            item.size = 10;

            // Act
            bool result = playerMechanics.AddItemToInventory(item);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AddItemToInventory_ItemDoesNotFit_ReturnsFalse()
        {
            // Arrange
            PlayerMechanics playerMechanics = new PlayerMechanics();
            Item item = new Item();
            item.size = 50;

            // Act
            bool result = playerMechanics.AddItemToInventory(item);

            // Assert
            Assert.IsFalse(result);
        }
    }
}