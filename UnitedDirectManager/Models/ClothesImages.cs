using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedDirectManager.Models
{
    class ClothesImages
    {
		private int _clothesImagesID;

		public int ClothesImagesID
		{
			get { return _clothesImagesID; }
			set { _clothesImagesID = value; }
		}

		private byte[] _imageFile;

		public byte[] ImageFile
		{
			get { return _imageFile; }
			set { _imageFile = value; }
		}

		private int _clothesId;

		public int ClothesId
		{
			get { return _clothesId; }
			set { _clothesId = value; }
		}
	}
}
