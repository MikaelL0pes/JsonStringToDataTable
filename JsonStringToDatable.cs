// Method to Process JSON
void ProcessJson(JToken token, DataRow row, string prefix, DataTable dataTable)
{
	if (token is JObject)
	{
		foreach (var prop in (JObject)token)
		{
			ProcessJson(prop.Value, row, string.IsNullOrEmpty(prefix) ? prop.Key : prefix + "_" + prop.Key, dataTable);
		}
	}
	else if (token is JArray)
	{
		if (!dataTable.Columns.Contains(prefix))
		{
			dataTable.Columns.Add(prefix, typeof(int));
		}
		row[prefix] = ((JArray)token).Count;
	}
	else
	{
		if (!dataTable.Columns.Contains(prefix))
		{
			dataTable.Columns.Add(prefix, typeof(string));
		}
		row[prefix] = (token ?? "").ToString();
	}
}

// Main method to process JSON and fill the DataTable
void Main(string jsonString, DataTable dataTable)
{
	JArray jsonArray = JsonConvert.DeserializeObject<JArray>(jsonString);

	foreach (JObject jsonObject in jsonArray)
	{
		DataRow newRow = dataTable.NewRow();
		ProcessJson(jsonObject, newRow, "", dataTable);
		dataTable.Rows.Add(newRow);
	}
}

// Initialize a new DataTable to store the processed JSON data
DataTable dataTable = new DataTable();


Main(jsonString, dataTable);

// Assign the filled DataTable to the dtJson variable as an output argument
dtJson = dataTable;
