using System.Text;
using Content.Server.Speech.Components;
using Content.Shared.Speech;
using Robust.Shared.Random;

namespace Content.Server.Speech.EntitySystems;

public sealed class MonkeyAccentSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<MonkeyAccentComponent, AccentGetEvent>(OnAccent);
    }

    public string Accentuate(string message)
    {
        var words = message.Split();
        var accentedMessage = new StringBuilder(message.Length + 2);

        for (var i = 0; i < words.Length; i++)
        {
            var word = words[i];

            if (_random.NextDouble() >= 0.5)
            {
                if (word.Length > 1)
                {
                    foreach (var _ in word)
                    {
                        accentedMessage.Append('По');  // Corvax-Localization
                    }

                    if (_random.NextDouble() >= 0.3)
                        accentedMessage.Append('о');  // Corvax-Localization
                }
                else
                    accentedMessage.Append('н');  // Corvax-Localization
            }
            else
            {
                foreach (var _ in word)
                {
                    if (_random.NextDouble() >= 0.8)
                        accentedMessage.Append('там');  // Corvax-Localization
                    else
                        accentedMessage.Append('пон');  // Corvax-Localization
                }

            }

            if (i < words.Length - 1)
                accentedMessage.Append(' ');
        }

        accentedMessage.Append('!');

        return accentedMessage.ToString();
    }

    private void OnAccent(EntityUid uid, MonkeyAccentComponent component, AccentGetEvent args)
    {
        args.Message = Accentuate(args.Message);
    }
}
