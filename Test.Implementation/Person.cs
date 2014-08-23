//*********************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    
//    Apache 2.0 License
//    
//    You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
//    
//    Unless required by applicable law or agreed to in writing, software 
//    distributed under the License is distributed on an "AS IS" BASIS, 
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or 
//    implied. See the License for the specific language governing 
//    permissions and limitations under the License.
//
//*********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Orleans;
using Test.Interfaces;

namespace Test.Implementation
{
    [StorageProvider(ProviderName = "DocumentDBStore")]
    public class Person : GrainBase<IPersonState>, Test.Interfaces.IPerson
    {
        Task IPerson.Register(PersonalAttributes props)
        {
            this.State.FirstName = props.FirstName;
            this.State.LastName = props.LastName;
            this.State.Gender = props.Gender;

            return this.State.WriteStateAsync();
        }

        Task IPerson.Marry(Guid spouseId, string newLastName)
        {
            this.State.IsMarried = true;
            this.State.LastName = newLastName;

            return this.State.WriteStateAsync();
        }

        Task<string> IPerson.FirstName
        {
            get { return Task.FromResult(State.FirstName); }
        }

        Task<string> IPerson.LastName
        {
            get { return Task.FromResult(State.LastName); }
        }

        Task<GenderType> IPerson.Gender
        {
            get { return Task.FromResult(State.Gender); }
        }

        Task<bool> IPerson.IsMarried
        {
            get { return Task.FromResult(State.IsMarried); }
        }
    }

    public interface IPersonState : IGrainState
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        GenderType Gender { get; set; }
        bool IsMarried { get; set; }
    }
}
