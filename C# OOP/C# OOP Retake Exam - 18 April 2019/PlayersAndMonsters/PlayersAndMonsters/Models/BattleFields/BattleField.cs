using PlayersAndMonsters.Common;
using PlayersAndMonsters.Models.BattleFields.Contracts;
using PlayersAndMonsters.Models.Players.Contracts;
using PlayersAndMonsters.Models.Players.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayersAndMonsters.Models.BattleFields
{
    public class BattleField : IBattleField
    {
        public void Fight(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            IsOnePlayerDead(attackPlayer,enemyPlayer);
            IncreaseHealthAndDamagePoints(attackPlayer, enemyPlayer);
            IncreaseHealthPionts(attackPlayer,enemyPlayer);
            while (true)
            {
                int attackDamage = attackPlayer.CardRepository.Cards.Sum(c => c.DamagePoints);

                enemyPlayer.TakeDamage(attackDamage);

                if (enemyPlayer.IsDead == true)
                {
                    break;
                }
                int enemyDamage = enemyPlayer.CardRepository.Cards.Sum(c => c.DamagePoints);
                attackPlayer.TakeDamage(enemyDamage);
                if (attackPlayer.IsDead == true)
                {
                    break;
                }
            }
        }

        private void IncreaseHealthPionts(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            attackPlayer.Health += attackPlayer.CardRepository.Cards.Sum(c => c.HealthPoints);
            enemyPlayer.Health += enemyPlayer.CardRepository.Cards.Sum(c => c.HealthPoints);
        }

        private void IncreaseHealthAndDamagePoints(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            if (attackPlayer.GetType().Name == nameof(Beginner))
            {
                attackPlayer.Health += 40;
                foreach (var card in attackPlayer.CardRepository.Cards)
                {
                    card.DamagePoints += 30;
                }
            }
            if (enemyPlayer.GetType().Name == nameof(Beginner))
            {
                enemyPlayer.Health += 40;
                foreach (var card in enemyPlayer.CardRepository.Cards)
                {
                    card.DamagePoints += 30;
                }
            }
        }

        private void IsOnePlayerDead(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            if (attackPlayer.IsDead == true)
            {
                throw new ArgumentException(ExceptionMessages.DeathPlayer);
            }
            if (enemyPlayer.IsDead == true)
            {
                throw new ArgumentException(ExceptionMessages.DeathPlayer);
            }
        }
    }
}
