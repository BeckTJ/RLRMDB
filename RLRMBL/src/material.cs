using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;



namespace RLRMBL;

public class material
{
    List<material> M = new List<material>();
    Connection con = new Connection();
    public int number { get; set; }
    public string name { get; set; }
    public string nameAbreviation { get; set; }
    public string permitNumber { get; set; }
    public string grade { get; set; }
    public string unitOfIssue { get; set; }
    public bool carbonDrumRequired { get; set; }
    public bool batchManaged { get; set; }
    public bool isRawMaterial { get; set; }

    public material()
    {
        number = 0;
        name = "";
        nameAbreviation = "";
        permitNumber = "";
        grade = "";
        unitOfIssue = "";
        carbonDrumRequired = false;
        batchManaged = false;
        isRawMaterial = false;
    }

    public void getMaterialFromDatabase()
    {
        int index;
        string query = @"Select * 
                            FROM materialNumber 
                            JOIN materialName ON materialNumber.materialNameId = MaterialName.MaterialNameId";
        con.OpenConnection();
        SqlDataReader reader = con.DataReader(query);

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                material mat = new material();
                mat.number = reader.GetInt32(reader.GetOrdinal("materialNumber"));
                mat.name = reader.GetString(reader.GetOrdinal("materialName"));
                mat.nameAbreviation = reader.GetString(reader.GetOrdinal("materialNameAbreviation"));
                index = reader.GetOrdinal("permitNumber");
                if (!reader.IsDBNull(index))
                {
                    mat.permitNumber = reader.GetString(reader.GetOrdinal("permitNumber"));
                }
                index = reader.GetOrdinal("materialGrade");
                if (!reader.IsDBNull(index))
                {
                    mat.grade = reader.GetString(reader.GetOrdinal("materialGrade"));
                }
                mat.unitOfIssue = reader.GetString(reader.GetOrdinal("unitOfIssue"));
                mat.carbonDrumRequired = Convert.ToBoolean(reader.GetInt32(reader.GetOrdinal("carbonDrumRequired")));
                mat.batchManaged = Convert.ToBoolean(reader.GetInt32(reader.GetOrdinal("batchManaged")));
                mat.isRawMaterial = Convert.ToBoolean(reader.GetInt32(reader.GetOrdinal("isRawMaterial")));
                M.Add(mat);
            }

        }
        con.CloseConnection();
    }
    public List<material> getMaterial()
    {
        getMaterialFromDatabase();
        return M;
    }
    public material getSingleMaterial(int input)
    {
        material soloMaterial = new material();

        int index;
        string query = @"Select *
                            FROM materialNumber 
                            JOIN materialName ON materialNumber.materialNameId = MaterialName.MaterialNameId
                            WHERE materialNumber = " + input;
        con.OpenConnection();
        SqlDataReader reader = con.DataReader(query);
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                soloMaterial.number = reader.GetInt32(reader.GetOrdinal("materialNumber"));
                soloMaterial.name = reader.GetString(reader.GetOrdinal("materialName"));
                soloMaterial.nameAbreviation = reader.GetString(reader.GetOrdinal("materialNameAbreviation"));
                index = reader.GetOrdinal("permitNumber");
                if (!reader.IsDBNull(index))
                {
                    soloMaterial.permitNumber = reader.GetString(reader.GetOrdinal("permitNumber"));
                }
                index = reader.GetOrdinal("materialGrade");
                if (!reader.IsDBNull(index))
                {
                    soloMaterial.grade = reader.GetString(reader.GetOrdinal("materialGrade"));
                }
                soloMaterial.unitOfIssue = reader.GetString(reader.GetOrdinal("unitOfIssue"));
                soloMaterial.carbonDrumRequired = (Convert.ToBoolean(reader.GetInt32(reader.GetOrdinal("carbonDrumRequired"))));
                soloMaterial.batchManaged = Convert.ToBoolean(reader.GetInt32(reader.GetOrdinal("batchManaged")));
                soloMaterial.isRawMaterial = Convert.ToBoolean(reader.GetInt32(reader.GetOrdinal("isRawMaterial")));
            }
        }
        con.CloseConnection();
        Console.WriteLine(soloMaterial.name);
        return soloMaterial;
    }

    public void setMaterialName(int materialNameId, string name, string nameAbreviation, string permitNumber, string rawMaterialCode, string productCode, bool CarbonDrumRequired, int carbonDrumDaysAllowed, int carbonDrumWeightAllowed)
    {
        con.OpenConnection();
        string query = @"INSERT INTO materialName(materialNameId, materialName, materialNameAbreviation, permitNumber, rawMaterialCode, productCode, CarbonDrumRequired, carbonDrumDaysAllowed, carbonDrumWeightAllowed),
                        VALUES(" + materialNameId + "," + name + "," + nameAbreviation + "," + permitNumber + "," + rawMaterialCode + "," + productCode + "," + Convert.ToInt32(CarbonDrumRequired) + "," + carbonDrumDaysAllowed + "," + carbonDrumWeightAllowed + ")";

        con.executeQuery(query);
        con.CloseConnection();

    }
    public void setMaterialNumber(int number, int materialNameId, string grade, int unitOfIssue, bool batchManaged, bool processOrderRequired, bool isRawMaterial)
    {
        con.OpenConnection();
        string query = @"INSERT INTO materialNumber(materialNumber, materialNameId, materialGrade, batchManaged, requiresProcessOrder, unitOfIssue, isRawMaterial),
                       VALUES(" + number + "," + materialNameId + "," + grade + "," + Convert.ToInt32(batchManaged) + "," + Convert.ToInt32(processOrderRequired) + "," + unitOfIssue + "," + Convert.ToInt32(isRawMaterial) + ")";
        con.executeQuery(query);
        con.CloseConnection();
    }

}
