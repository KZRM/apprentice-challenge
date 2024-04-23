using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using static HolidayCalendar.HolidayCalendar;


namespace HolidayCalendar;
public class HolidayCalendar : IHolidayCalendar
{
    //Create httpClient...
    private static HttpClient httpClient = new HttpClient();

    //Generated bearer token...
    string bearerToken = "424caf23-1892-4697-ba34-4aa83abaca36";

    //Made class for recieved url string data...
    public class Holiday
    {
        public string Date { get; set; }
        public string Name { get; set; }
        public bool NationalHoliday { get; set; }
    }



    public bool IsHoliday(DateTime date)
    {
        var holidays = GetHolidays(date, date);
        /* ^ I could also have used their single date check instead: https://api.sallinggroup.com/v1/holidays/is-holiday?date=2018-12-24 -H 'Authorization: Bearer <YOUR-TOKEN>'
          - But this seems to work fine */

        bool hasHoliday = holidays.Contains(date);
        return hasHoliday;
    }






    public ICollection<DateTime> GetHolidays(DateTime startDate, DateTime endDate)
    {

        //Authenticate HttpClient...
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

        //Send request url...
        string requestUrl = $"https://api.sallinggroup.com/v1/holidays?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";
        //* For testing */Console.WriteLine($"Requesting URL: {requestUrl}");

        //Store recieved response...
        string recievedJsonStringData = httpClient.GetStringAsync(requestUrl).Result;
        //* For testing */Console.WriteLine("The salling group list of holidays are" + storedResponse);


        //Convert from JSON to C#
        List<Holiday> holidays = JsonConvert.DeserializeObject<List<Holiday>>(recievedJsonStringData);

        //Make Datetime list...
        List<DateTime> holidayDates = new List<DateTime>();
        foreach (Holiday holiday in holidays)
        {
            //String for message in "Test explorer" 
            string isOrIsNot;

            //Add all national holidays to DateTime holidaySates list...
            if (holiday.NationalHoliday)
            {
                //Convert string data to DateTime variable...
                DateTime date = DateTime.Parse(holiday.Date);
                holidayDates.Add(date);
                isOrIsNot = "is";
            }
            else
            { isOrIsNot = "is NOT"; }

            /* Log dates: */
            Console.WriteLine($"the date {holiday.Date}, is called {holiday.Name}, and {isOrIsNot} a national holiday!");
        }

        return holidayDates;

    }


}
