using System.Threading.Tasks;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class CheckCardIdService : IcheckCardIdService
    {
        public async Task<bool> IsRealAsync(string cardId)
        {
            await Task.Delay(0);
            int[] cardIdArray = new int[9];
            if (cardId.Length != 9)
            {
                return false;
            }
            for (int i = 0; i < cardId.Length; i++)
            {
                if (!(char.IsDigit(cardId[i])))
                {
                    return false;
                }
                cardIdArray[i] = cardId[i] - 48;
            }
            if (cardIdArray[0] != 6)
            {
                return false;
            }

            return true;

        }
    }
}
