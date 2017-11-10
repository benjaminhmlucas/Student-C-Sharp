using SimpleLinkedList;
using System;
using System.IO;

namespace PatientData
{
    class PatientData
    {
        private SimpleLinkedList<Patient>[] list;

        /// <summary>
        /// Constructor.  Instantiates the array based on the size passed to the constructor.
        /// </summary>
        /// <param name="size">The size of the array.</param>
        public PatientData(int size)
        {
            list = new SimpleLinkedList<Patient>[size];
        }

        /// <summary>
        /// Loads patients into the data structure from the filename passed as a parameter.
        /// </summary>
        /// <param name="filename">Path to the file where the patient data is kept.  The format is: <para>first,last,procedureCost</para></param>
        public void LoadPatients(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            StreamReader input = new StreamReader(fs);

            while (!input.EndOfStream)
            {
                string line = input.ReadLine();
                string[] fields = line.Split(",".ToCharArray());
                Patient newPat = new Patient(fields[1],fields[0],double.Parse(fields[2]),Patient.genPatientId(fields[1], fields[0]));// TODO create the new patient
                // TODO add the patatient to the list
                AddPatient(newPat);
            }
        }

        /// <summary>
        /// Adds a patient to the data structure.
        /// </summary>
        /// <param name="patient">The patient to be added.</param>
        public void AddPatient(Patient patient)
        {
            Patient pat = patient;
            int patIndex = pat.Id % list.Length;// Find the correct index
            if (list[patIndex] == null) {// If no list exists at specified index, create a new one
                list[patIndex] = new SimpleLinkedList<Patient>();// Create a new list if there isn't one at this index
            }
            list[patIndex].AddAtHead(pat);// Add the patient to the linked list
            
        }
        /// <summary>
        /// Finds a patient from the list based on name
        /// </summary>
        /// <param name="first">first name of patient</param>
        /// <param name="last">last name of patient</param>
        public Patient FindPatientByName(string first, string last)
        {
            Patient pat = null;            
            Patient targetPatient = new Patient(first,last,0, Patient.genPatientId(first, last));// creates a dummy patient for search
            int ndx = targetPatient.Id % list.Length;//find index of patient in array
            if (list[ndx] == null) {
                list[ndx] = new SimpleLinkedList<Patient>();
            }
            if (list[ndx].Find(targetPatient) != null)//if patient is found,
            {
                pat = list[ndx].Find(targetPatient);//assign patient to a reference variable
            }
            return pat;// return patient reference variable, returns null if not found
        }
        /// <summary>
        /// Find the number of slots used in the main simple linked list array
        /// </summary>
        /// <param name="size">size of array</param>
        public double FindPercentUsedSlots(int size)
        {
            double usedCount = 0;
            double percentageUsed;

            foreach (Object o in list)
            {
                if (o != null)
                {
                    usedCount++;
                }    
            }
            percentageUsed = usedCount / size;
            return percentageUsed;
        }
        /// <summary>
        /// searches each inner list and finds the length of the longest linked list
        /// </summary>        
        public int FindLongestLength()
        {
            int max = 0;
            int currentList;
            foreach (SimpleLinkedList<Patient> patientList in list)
            {
                if (patientList != null)
                {
                    currentList = patientList.Count;
                    if (currentList > max)
                    {
                        max = currentList;
                    }
                }
            }
            return max;
        }
        /// <summary>
        /// searches each inner list and finds the average length all linked lists in the list array
        /// </summary>
        public double FindAverageLength()
        {
            double avg;
            int lengthSum = 0;//sums totals of length totals for each linked list in array
            int counter = 0;//nummber of used linked lists in array
            foreach (SimpleLinkedList<Patient> patientList in list)
            {
                if (patientList != null)
                {
                    lengthSum += patientList.Count;
                    counter++;
                }
            }
            avg = lengthSum / counter;
            return avg;
        }
        /// <summary>
        /// finds index in the top array that the patient was stored in using the patients id
        /// and removes patient.  The patients data is returned so it can be displayed one more time
        /// </summary>
        /// <param name="inPat">patient to remove</param>
        public Patient RemovePatient(Patient inPat)
        {            
            int ndx = inPat.Id % list.Length;
            list[ndx].Remove(inPat);// remove the patient            
            return inPat;// return the data
        }
    }
}
