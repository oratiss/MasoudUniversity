using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasoudUniversity.Models
{
    public class ClassPropertiesValidator
    {
        private string _ClassIdentifier;
        private const int ExpectedIdentifierCharactersLength = 4;
        private bool LocationNameStartingPhrase;

        public bool CheckValidityOfClassIdentifier(string Id, string Location)
        {
            this._ClassIdentifier = Id;

            if (this._ClassIdentifier.Length != ExpectedIdentifierCharactersLength)
            {
                return false;
            }

            if (this._ClassIdentifier is null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            foreach (char c in this._ClassIdentifier)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
                    
            }

            if (!(Location.Trim().ToLower().StartsWith("building"))|| (Location.Trim().ToLower().StartsWith("yard"))|| (Location.Trim().ToLower().StartsWith("library")))
            {
                return false;
            }

            return true;
        }

    }
}
