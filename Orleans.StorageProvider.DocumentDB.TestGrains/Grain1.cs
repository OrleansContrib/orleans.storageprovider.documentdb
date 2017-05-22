using Orleans.Providers;
using System;
using System.Threading.Tasks;

namespace Orleans.StorageProvider.DocumentDB.TestGrains
{
    public interface IGrain1 : IGrainWithStringKey
    {
        Task Set(string stringValue, int intValue, DateTime dateTimeValue, Guid guidValue, IGrain1 grainRef);
        Task<Tuple<string, int, DateTime, Guid, IGrain1>> Get();
        Task Clear();
    }

    public class MyState 
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public DateTime DateTimeValue { get; set; }
        public Guid GuidValue { get; set; }
        public IGrain1 GrainRef { get; set; }
    }

    [StorageProvider(ProviderName = "ARANGO")]
    public class Grain1 : Grain<MyState>, IGrain1
    {
        public Task Set(string stringValue, int intValue, DateTime dateTimeValue, Guid guidValue, IGrain1 grainRef)
        {
            try
            {
                this.State.StringValue = stringValue;
                this.State.IntValue = intValue;
                this.State.DateTimeValue = dateTimeValue;
                this.State.GuidValue = guidValue;
                this.State.GrainRef = grainRef;
                return WriteStateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }


        }

        public Task<Tuple<string, int, DateTime, Guid, IGrain1>> Get()
        {
            try
            {
                this.ReadStateAsync();

                return Task.FromResult(new Tuple<string, int, DateTime, Guid, IGrain1>(
                  this.State.StringValue,
                  this.State.IntValue,
                  this.State.DateTimeValue,
                  this.State.GuidValue,
                  this.State.GrainRef));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task Clear()
        {
            try
            {
                await ClearStateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
