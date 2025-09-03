using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public static class TZ_21_07_2025Context_General
    {
        public static Models.TZ_21_07_2025Context base_TZ_21_07_2025Context(this Models.TZ_21_07_2025Context context) 
        {
            return context;
        }
        public static Procedures.TZ_21_07_2025Context base_TZ_21_07_2025Context(this Procedures.TZ_21_07_2025Context context) 
        {
            return context;
        }
        public static Views.TZ_21_07_2025Context base_TZ_21_07_2025Context(this Views.TZ_21_07_2025Context context)
        {
            return context;
        }
    }
}