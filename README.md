# AspNetCoreLibrary.DataAccess
DataAccess

### Initialize client
`DataAccessSybase<ResponseObj> client;`  
```
client = new DataAccessSybase<ResponseObj>("connectionString");
client = new DataAccessSql<ResponseObj>("connectionString");
client = new DataAccessOracle<ResponseObj>("connectionString");
```
#

### Request data from database
```
ResponseObj count = client.ExecuteScalar("query", CommandType.Text);
int affected = client.ExecuteNonQuery("query", CommandType.Text);
ResponseObj resp = client.ExecuteReader("store-procedure-name", CommandType.StoredProcedure);
```
#

### Add command parameter 
* input param
```
client.comm.Parameters.AddWithValue("@customerId", SqlDbType.VarChar).Value = customer.id;
```
* output param
```
SqlParameter paramName = new SqlParameter("@sptid", SqlDbType.VarChar);
paramName.Direction = ParameterDirection.Output;
client.comm.Parameters.Add(paramName);
string value = paramName.Value.ToString();
```
* return value
```
SqlParameter returnValue = new SqlParameter("@sptid", AseDbType.VarChar);
returnValue.Direction = ParameterDirection.ReturnValue;
client.comm.Parameters.Add(returnValue);
string value = returnValue.Value.ToString();
```
#



### Information
* **ResponseObj** is the expected data object where the properties(name & type) are same defined on the table
* **CommandType** can be CommandType.Text || CommandType.StoredProcedure






