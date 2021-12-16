namespace Parkeringshuset.Controllers
{
    using Parkeringshuset.Data;
    using System.Linq;

    public class ParkingTypeController
    {
        public ParkeringGarageContext db = new();
        #region Read
        /// <summary>
        /// Get how many free spots there is for a specific parking type. 
        /// </summary>
        /// <param name="Ptype">the name of the parking type.</param>
        /// <returns></returns>
        public int ReadFreeSpots(string Ptype)
        {
            var ptype = db.Ptypes.FirstOrDefault(p => p.Name == Ptype);

            if (ptype is not null)
            {
                return ptype.TotalSpots - ptype.Used;
            }
            else
            {
                return default;
            }
        }
        #endregion
    }
}