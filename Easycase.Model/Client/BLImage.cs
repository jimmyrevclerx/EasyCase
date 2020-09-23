using Easycase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Model.Client
{
    public class BLImage : IDisposable
    {
        public long ID { get; set; }
        public string ImageName { get; set; }
        public string OriginalName { get; set; }
        public string ImagePath { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }

        /// <summary>
        /// Save image
        /// </summary>
        /// <returns></returns>
        public long Save()
        {
            try
            {
                long id = 0;
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var image = new DataModel.Image
                    {
                        ImageName = this.ImageName,
                        OriginalName = this.OriginalName,
                        ImagePath = this.ImagePath,
                        CreatedOn = DateTime.Now,
                    };
                    DB.Images.Add(image);
                    DB.SaveChanges();
                    id = image.ID;
                }
                return id;
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Get image by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BLImage GetById(long id)
        {
            try
            {
                using (Easycase.DataModel.EasyCaseDBEntities DB = new DataModel.EasyCaseDBEntities())
                {
                    var client = DB.Images.Where(d => d.ID == id).Select(c => new BLImage
                    {
                        ID = c.ID,
                        ImageName=c.ImageName,
                        ImagePath=c.ImagePath,
                        OriginalName=c.OriginalName
                    }).FirstOrDefault();
                    return client;
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.Message);
                return new BLImage();
            }
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
