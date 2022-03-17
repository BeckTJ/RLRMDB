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
    public int sequenceIdEnd { get; set; }
    public int sequenceIdStart { get; set; }
    public string vendor { get; set; }

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
        vendor = "";
    }

    public void materialFromDatabase()
    {
        int index;
        string query = @"Select * 
                            FROM materialNumber 
                            JOIN materialName ON materialNumber.materialNameId = MaterialName.MaterialNameId
                            JOIN materialId ON materialNumber.materialNumber = materialId.materialNumber
                            JOIN productNumberSequence ON materialId.sequenceId = productNumberSequence.sequenceId
                            JOIN vendor ON vendor.vendorId = materialId.vendorId";
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
                mat.carbonDrumRequired = reader.GetBoolean(reader.GetOrdinal("carbonDrumRequired"));
                mat.batchManaged = reader.GetBoolean(reader.GetOrdinal("batchManaged"));
                mat.isRawMaterial = reader.GetBoolean(reader.GetOrdinal("isRawMaterial"));
                M.Add(mat);
            }

        }
        con.CloseConnection();
    }
    public List<material> getMaterial()
    {
        materialFromDatabase();
        return M;
    }
    public material getSingleMaterialFromDatabase(int number, int id)
    {
        material soloMaterial = new material();

        int index;
        string query = @"Select *
                            FROM materialNumber 
                            JOIN materialName ON materialNumber.materialNameId = materialName.materialNameId
                            JOIN materialId ON materialNumber.materialNumber = materialId.materialNumber
                            JOIN productNumberSequence ON materialId.sequenceId = productNumberSequence.sequenceId
                            JOIN vendor ON vendor.vendorId = materialId.vendorId
                            WHERE materialNumber.materialNumber = " + number + "AND vendor.vendorId =" + id;
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
                soloMaterial.carbonDrumRequired = (reader.GetBoolean(reader.GetOrdinal("carbonDrumRequired")));
                soloMaterial.batchManaged = reader.GetBoolean(reader.GetOrdinal("batchManaged"));
                soloMaterial.isRawMaterial = reader.GetBoolean(reader.GetOrdinal("isRawMaterial"));
                soloMaterial.sequenceIdEnd = reader.GetInt32(reader.GetOrdinal("sequenceIdEnd"));
                soloMaterial.sequenceIdStart = reader.GetInt32(reader.GetOrdinal("sequenceIdStart"));
                soloMaterial.vendor = reader.GetString(reader.GetOrdinal("vendorName"));
            }
        }
        con.CloseConnection();
        return soloMaterial;
    }

    public void addMaterialToDatabase(int number, string name, string abreviation, string permit, string rmCode, string pCode, bool requireCarbonDrum, int cdDaysAllowed, int cdWeightAllowed, string grade, bool batch, bool processOrder, string UI, bool isRM, string vendorName, int sequenceNumber)
    {
        con.OpenConnection();
        con.executeStoredProcedure(@"materialInsert " + number + ", '" + name + "','" + abreviation + "','" + permit + "','" + rmCode + "','" + pCode + "'," + requireCarbonDrum + "," + cdDaysAllowed + "," + cdWeightAllowed + "," + grade + "," + batch + "," + processOrder + ",'" + UI + "'," + isRM + ",'" + vendorName + "'," + sequenceNumber);
        con.CloseConnection();
    }

}

