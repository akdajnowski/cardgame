using System.Collections.Generic;
using System.Linq;

public class CardReducer
{
    public struct Resolution
    {
        public int PlayerDamageTaken;
        public int OpponentDamageTaken;
    }

    public Resolution ResolveCard(CardDescriptor playerCard, CardDescriptor opponentCard)
    {
        var player = FlattenAttr(playerCard.CardAttributes);
        var opponent = FlattenAttr(opponentCard.CardAttributes);

        return new Resolution
        {
            OpponentDamageTaken = ResolveDamage(player, opponent),
            PlayerDamageTaken = ResolveDamage(opponent, player),
        };
    }

    private static int ResolveDamage(List<CardAttribute.Type> attacker, List<CardAttribute.Type> defender)
    {
        var physDealtDamage = (attacker.Count(q => q == CardAttribute.Type.Attack) - defender.Count(q => q == CardAttribute.Type.Defense)).NonNegative();
        var magDealtDamage = (attacker.Count(q => q == CardAttribute.Type.MagicAttack) - defender.Count(q => q == CardAttribute.Type.MagicDefense)).NonNegative();
        var heal = defender.Count(q => q == CardAttribute.Type.Heal);

        var damageTakenOverall = (physDealtDamage + magDealtDamage) - heal;
        return damageTakenOverall;
    }

    private List<CardAttribute.Type> FlattenAttr(List<CardAttribute> attributes)
    {
        return attributes.SelectMany(s => Enumerable.Range(1, s.Quantity).Select(i => s.Quality)).ToList();
    }
}
