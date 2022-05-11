using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
    public class WarController
    {
        private readonly List<Character> characterParty;
        private readonly Stack<Item> itemPool;

        public WarController()
        {
            characterParty = new List<Character>();
            itemPool = new Stack<Item>();
        }

        public string JoinParty(string[] args)
        {
            string characterType = args[0];
            string name = args[1];
            Character character;
            if (characterType == nameof(Warrior))
            {
                character = new Warrior(name);
            }
            else if (characterType == nameof(Priest))
            {
                character = new Priest(name);
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType, characterType));
            }

            characterParty.Add(character);
            return string.Format(SuccessMessages.JoinParty, name);
        }

        public string AddItemToPool(string[] args)
        {
            string itemName = args[0];
            Item item;
            if (itemName == nameof(FirePotion))
            {
                item = new FirePotion();
            }
            else if (itemName == nameof(HealthPotion))
            {
                item = new HealthPotion();
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, itemName));
            }

            itemPool.Push(item);
            return string.Format(SuccessMessages.AddItemToPool, itemName);
        }

        public string PickUpItem(string[] args)
        {
            string characterName = args[0];
            var character = characterParty.FirstOrDefault(x => x.Name == characterName);

            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }

            if (!itemPool.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
            }

            var item = itemPool.Pop();
            character.Bag.AddItem(item);
            return string.Format(SuccessMessages.PickUpItem, characterName, item.GetType().Name);
        }

        public string UseItem(string[] args)
        {
            string characterName = args[0];
            string itemName = args[1];
            var character = characterParty.FirstOrDefault(x => x.Name == characterName);

            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }

            var item = character.Bag.GetItem(itemName);
            character.UseItem(item);
            return string.Format(SuccessMessages.UsedItem, characterName, itemName);
        }

        public string GetStats()
        {
            StringBuilder stats = new StringBuilder();
            foreach (var character in characterParty.
                OrderByDescending(x => x.IsAlive).
                ThenByDescending(x => x.Health))
            {
                stats.AppendLine(string.Format(
                    SuccessMessages.CharacterStats,
                    character.Name,
                    character.Health,
                    character.BaseHealth,
                    character.Armor,
                    character.BaseArmor,
                    character.IsAlive ? "Alive" : "Dead"));
            }

            return stats.ToString().TrimEnd();
        }

        public string Attack(string[] args)
        {
            string attackerName = args[0];
            string receiverName = args[1];

            var attacker = characterParty.FirstOrDefault(x => x.Name == attackerName);
            var receiver = characterParty.FirstOrDefault(x => x.Name == receiverName);

            if (attacker == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, attackerName));
            }
            if (receiver == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, receiverName));
            }

            if (attacker is IAttacker warrior)
            {
                warrior.Attack(receiver);
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, attackerName));
            }

            string result = string.Format(
                SuccessMessages.AttackCharacter,
                attackerName,
                receiverName,
                attacker.AbilityPoints,
                receiverName,
                receiver.Health,
                receiver.BaseHealth,
                receiver.Armor,
                receiver.BaseArmor);

            return receiver.IsAlive ? result :
                result +
                Environment.NewLine +
                string.Format(SuccessMessages.AttackKillsCharacter, receiverName);
        }

        public string Heal(string[] args)
        {
            string healerName = args[0];
            string healingReceiverName = args[1];

            var healer = characterParty.FirstOrDefault(x => x.Name == healerName);
            var healingReceiver = characterParty.FirstOrDefault(x => x.Name == healingReceiverName);

            if (healer == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healerName));
            }
            if (healingReceiver == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healingReceiverName));
            }

            if (healer is IHealer priest)
            {
                priest.Heal(healingReceiver);
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, healerName));
            }

            string result = string.Format(
                SuccessMessages.HealCharacter,
                healerName,
                healingReceiverName,
                healer.AbilityPoints,
                healingReceiverName,
                healingReceiver.Health);

            return result;
        }
    }
}
