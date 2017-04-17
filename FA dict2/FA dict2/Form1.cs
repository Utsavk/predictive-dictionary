using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Data.SqlClient;

namespace FA_dict2
{
    public partial class Form1 : Form
    {
        String project_path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        class node
        {
            private char character;
            public char data
            {
                get
                {
                    return this.character;
                }
                private set
                {
                    this.character = value;
                }
            }

            public node kid, sibling;
            public node(char c)
            {
                this.data = c;
                this.kid = this.sibling = null;
            }
        }
        Hashtable hashtable = new Hashtable();
        Hashtable hashtable2 = new Hashtable();
        int s_no = 0;
        node root, save;
        struct definition
        {
            private int frequency;
            public string meaning;
            public definition(int f, string m)
            {
                this.meaning = m;
                this.frequency = f;
            }
            public int freq
            {
                get
                {
                    return this.frequency;
                }
            }
            public void incfreq()
            {
                this.frequency += 1;
            }
        };
        List<definition> meaning_list = new List<definition>();
        
        private void createtrie(string[] words,string[] meanings)
        {
            
            int lim = words.Length;
            int wind = 0;
            int meanind = 0;
            int limit = words.Length;
            
            for(wind=0;wind<limit;wind++)
            {
                string w = words[wind];
                hashtable2.Add(w, meanings[meanind]);
                char[] ar = w.ToCharArray();
                node t = root;
                int index = 0;
                foreach (var i in ar)//var is char
                {
                    index++;
                    node newnode = new node(i);
                    t = insert(t, newnode);
                    if (index == ar.Length)
                    {
                        newnode = new node('#');//Marks the end of the word
                        t = insert(t, newnode);
                    }

                }//A word is created
                hashtable.Add(w, s_no++);
                definition freq_and_mean = new definition(0, "no");
                meaning_list.Add(freq_and_mean);
                meanind += 2;
                //feed_to_db(w);
            }
        }
       
        private node insert(node trav, node nnode)//during insertion of first character of a word, trav contains he root
        {

            if (trav.kid == null)
            {
                trav.kid = nnode;
                return trav.kid;
            }
            else
            {
                node n;
                save = trav.kid;
                n = trav.kid;
                while (n != null)
                {
                    if (n.data == nnode.data)
                    {
                        return (n);
                    }
                    n = n.sibling;
                }
                trav.kid = nnode;
                nnode.sibling = save;
                return trav.kid;
            }
        }
        private void display(node ptr, string prev)
        {

            node np = ptr;
            while (np != null)
            {
                if ((np != null) && (np.sibling != null))
                {
                    display(np.sibling, prev);
                }
                prev += np.data.ToString();
                np = np.kid;
            }
        }
        private string sear = "";
        int correct_input = 0, incorrect_input = 0;
        private List<string> corrections;// = new List<string>();
        List<string> distinct;
        private List<string> recent_prediction;
        public int[,] levenshteindistance(string b, string a)
        {
            int[,] d = new int[(b.Length), (a.Length)];
            int l1 = a.Length, l2 = b.Length;
            for (int i = 0; i < b.Length; i++)
                d[i, 0] = i;
            for (int j = 0; j < a.Length; j++)
                d[0, j] = j;
            for (int j = 1; j < l1; j++)
            {
                for (int i = 1; i < l2; i++)
                {
                    if (b[i] == a[j])
                        d[i, j] = d[i - 1, j - 1];
                    else
                    {
                        d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + 1);

                    }
                }
            }

            return d;
        }
        private void did_u_mean(List<string> correct, string user_input)
        {
            if (correct == null) //the first character itself is not present in any word
            //hence the prediction method is not called and coorections and so does distinct list are empty
            {
                //"List is Empty"
                return;
            }

            int k = 0;
            int[] diffr = new int[correct.Count];
            //predictionbox.ResetText();
            foreach (var word in correct)
            {

                int[,] diffmat = levenshteindistance(word, user_input);

                diffr[k] = diffmat[word.Length - 1, user_input.Length - 1];

                k++;
            }
            int[] t = new int[correct.Count];
            for (k = 0; k < t.Length; k++)
                t[k] = k;
            List<string> sorted = new List<string>();
            int i, key, j = 0, key_t;
            for (i = 1; i < k; i++)
            {
                key = diffr[t[i]];
                key_t = t[i];
                j = i - 1;

                /* Move elements of A[0..i-1], that are greater than key, to one 
                   position ahead of their current position.
                   This loop will run at most k times */
                while (j >= 0 && diffr[t[j]] > key)
                {
                    t[j + 1] = t[j];
                    j = j - 1;
                }
                t[j + 1] = key_t;
            }

            if (correct_input > 0)
            {
                label1.ResetText();
                label1.Visible = true;
                label1.Text = "DID YOU MEAN?";
            }
            for (int v = 0; v < k; v++)
            {
                if (k > 5)//only nearest 5 elements are printed
                    k = 5;
                sorted.Add(correct[t[v]]);
                //if correct.count>5 then always print the nearest five elements
            }
            predictionbox.DataSource = null;
            predictionbox.DataSource = sorted;
        }
        private void sorted_predictions(List<string> wordlist, int xcord = 0)
        {

            int k = 0;
            int[] t = new int[wordlist.Count];
            
            //label2.Text = t.ToString();
            for (k = 0; k < t.Length; k++)
                t[k] = k;

            int i, key, j, key_t;
            int[] frqs = new int[wordlist.Count];
            k = 0;
            foreach (var word in wordlist)
            {
                int ind;
                
                int.TryParse((hashtable[word]).ToString(), out ind);
                definition d = meaning_list[ind];

                frqs[k] = d.freq;
                k++;
            }
            for (i = 1; i < k; i++)
            {
                key = frqs[t[i]];
                key_t = t[i];
                j = i - 1;

                while (j >= 0 && frqs[t[j]] < key)
                {
                    t[j + 1] = t[j];
                    j = j - 1;
                }
                t[j + 1] = key_t;
            }
            List<string> sorted = new List<string>();
            for (int v = 0; v < k; v++)
            {
                sorted.Add(wordlist[t[v]]);

            }
            predictionbox.DataSource = null;
            predictionbox.DataSource = sorted;
        }
        private bool searchcharwise(char ch, string prev, node tmp, out node t)//tmp is the currnet node to be checked
        {
            if (tmp == null)
            {
                t = null;
                return false;
            }
            if (tmp.data != '#')
            {
                if (ch == tmp.data)
                {
                    if (ch != '#')
                    {//no use of this if
                        list_of_nexts.Add(tmp);
                        t = tmp.kid;
                        prediction(t, prev);
                        sorted_predictions(recent_prediction);
                        return true;
                    }
                    else
                    {
                        label1.Text = "Sorry";
                    }
                }
                else
                {
                    while (tmp != null)
                    {
                        tmp = tmp.sibling;

                        if (tmp != null)
                        {
                            if (ch == tmp.data)
                            {
                                if (ch != '#')
                                {//# is not allowed
                                    list_of_nexts.Add(tmp);
                                    t = tmp.kid;
                                    prediction(t, prev);
                                    sorted_predictions(recent_prediction);
                                    return true;
                                }

                            }
                        }

                    }
                    label1.Text = "Sorry";
                }
            }
            else if (tmp.data == '#' && tmp.sibling != null)
            {
                while (tmp != null)
                {
                    tmp = tmp.sibling;

                    if (tmp != null)
                    {
                        if (ch == tmp.data)
                        {
                            if (ch != '#')
                            {//# is not allowed
                                list_of_nexts.Add(tmp);
                                t = tmp.kid;
                                prediction(t, prev);
                                sorted_predictions(recent_prediction);
                                return true;
                            }
                        }
                    }
                }
            }
            t = null;
            return false;
        }
        private void prediction(node ptr, string prev)
        {

            label1.Text = "PREDICTIONS....";
            node np = ptr;

            if (np != null)
            {
                node sv = np;
                while (np.data != '#')
                {

                    if ((np != null) && (np.sibling != null))
                    {

                        prediction(np.sibling, prev);
                    }
                    prev += np.data;
                    np = np.kid;

                }
                if ((np.data == '#') && (np.sibling != null))
                {

                    np = np.sibling;
                    while (np.data == '#')//again a hash on left
                    {
                        np = np.sibling;
                    }
                    if (np != null)
                    {
                        prediction(np, prev);
                    }
                }
            }
            corrections.Add(prev);
            recent_prediction.Add(prev);
        }
        bool cont = true;
        node next = null;
        int[] eleindex = new int[15];
        List<string> save_recent_pred;
        bool complete_word = false;
        List<node> list_of_nexts=new List<node>();
        private void inputbeforEnter(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
            }
            save_recent_pred = recent_prediction;
            recent_prediction = new List<string>();

            predictionbox.ResetText();
            if (e.KeyCode == Keys.Enter)
            {
                if (cont == false) //wrong spelling entered
                {
                    predictionbox.Visible = true;
                    predictionbox.ResetText();
                    did_u_mean(distinct, sear);
                }
                return;
            }
            if ((e.KeyCode != Keys.Enter)&&(e.KeyCode!=Keys.Back))//Excluding the enter key that is new line character
            {
                sear += ((char)(e.KeyValue + 32)).ToString();
            }
            if (cont == true)
            {
                
                char inpk = sear.ToCharArray()[sear.Length - 1];
                 
                cont = searchcharwise(inpk, sear, next, out next);
                 
                if (cont)
                {
                    
                    correct_input++;
                    correct_or_incorrect.Add(true);
                    eleindex[correct_input - 1] = corrections.Count();
                }
            }
            if ((cont == false) && (e.KeyCode != Keys.Enter))//&&(correct_input-incorrect_input>=1))
            {//word doestn't exist in the database
                incorrect_input++;
                correct_or_incorrect.Add(false);
                predictionbox.ResetText();
                predictionbox.Visible = false;
                if (correct_input <= 0)
                {
                    //first char mismatched
                }
                if ((correct_input >= 1))// && (incorrect_input == 1))
                {
                    if ((correct_input <= 2))
                    {
                        distinct = corrections.Distinct().ToList();
                    }
                    else
                    {
                        int delrange;
                        delrange = eleindex[correct_input - 1 - 2];
                        if (((corrections.Count > delrange)))
                            corrections.RemoveRange(0, delrange);
                        distinct = corrections.Distinct().ToList();
                    }
                }
            }
        }
        void inputafterenter()
        {
            

            if ((correct_input == 0) && (incorrect_input == 0))//no char entered
            {
                predictionbox.Visible = false;
                label1.Visible = false;
                return;
            }

            if ((cont == true) && (incorrect_input == 0))
            {
                if (next.data == '#')
                {
                    word_found(sear);
                    complete_word = true;
                }
                else
                {
                    while (next.sibling != null)//this special while loop is for those words
                    //which had their prefixes as an independent word in the database.
                    //eg:kill...the word kill has kil as a prefix which is in the trie 
                    {
                        next = next.sibling;
                        if (next.data == '#')
                        {
                            word_found(sear);
                            complete_word = true;
                            return;
                        }
                    }
                }
                if (complete_word == true)
                {
                    int i;
                    int.TryParse((hashtable[sear]).ToString(), out i);
                    definition d = meaning_list[i];
                    d.incfreq();
                    meaning_list.RemoveAt(i);
                    meaning_list.Insert(i, d);
                }
                if (complete_word == false)//incomplete word
                    let_me_complete_it();
                

            }
            if ((cont == true) && (complete_word != false))//correct match
            {
                predictionbox.ResetText();
                predictionbox.Visible = false;
                
            }
            refresh_for_next_word();
        }
        private void let_me_complete_it()
        {
            //if (complete_word == false)//incomplete word
            {
                label1.ResetText();
                label1.Visible = true;
                label1.Text = "Let me complete it";

                sorted_predictions(save_recent_pred);//distinct contains all the prdictions upto the last input character
                
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        public Form1()
        {
            InitializeComponent();
             
            predictionbox.Visible = false;
            sear = "";
            correct_input = 0; incorrect_input = 0;
            corrections = new List<string>();
            recent_prediction = new List<string>();
            root = new node('*');
            string[] words = File.ReadAllLines
                (project_path+"\\word_meanings\\words.txt");
            string[] meanings = File.ReadAllLines
                (project_path + "\\word_meanings\\meanings.txt");

            
            root = new node('*');
            createtrie(words,meanings);
            label1.Text = " ";
            next = root.kid;
        }
        List<bool> correct_or_incorrect=new List<bool>();
        private void refresh_for_next_word()
        {
            sear = "";
            correct_input = 0; incorrect_input = 0;
            corrections = new List<string>();
            list_of_nexts = new List<node>();
            cont = true;
            eleindex = new int[15];
            next = root.kid;
            recent_prediction = new List<string>();
            searchbox.Text = "";
            if (complete_word == true)
                label1.Visible = false;
            downupkey = false;
            muv_down_up = 0;
            down_up_never_done_yet = false;
            correct_or_incorrect = new List<bool>();
            prev_muv_down_up = -1;
            prevdirection = false;//down
            click_instead_of_enter = false;
        }
        public string word_searched;
        private void word_found(string querry)
        {
            word_searched = querry;
            label1.Visible = false;
            Mean_box.Visible = true;
            Mean_box.ResetText();
            string s = hashtable2[querry].ToString();


            if (s.Length > 150)
            {
                s = s.Substring(0, 30) + "\n" + s.Substring(30, 30) + "\n" + s.Substring(60, 30) + "\n" +
                    s.Substring(90, 30) + "\n" + s.Substring(120, 30) + "\n" + s.Substring(150);
            }
            else if (s.Length > 120)
            {
                s = s.Substring(0, 30) + "\n" + s.Substring(30, 30) + "\n" + s.Substring(60, 30) + "\n" +
                    s.Substring(90, 30) + "\n" + s.Substring(120);
            }
            else if (s.Length > 90)
            {
                s = s.Substring(0, 30) + "\n" + s.Substring(30, 30) + "\n" + s.Substring(60, 30) + "\n" + s.Substring(90);
            }
            else if (s.Length > 60)
            {
                s = s.Substring(0, 30) + "\n" + s.Substring(30, 30) + "\n" + s.Substring(60);

            }
            else if (s.Length > 30)
            {
                s = s.Substring(0, 30) + "\n" + s.Substring(30);

            }
            Mean_box.Text = s;
        }
        private void item_selected()
        {
            string curItem = predictionbox.SelectedItem.ToString();
            word_found(curItem);
            predictionbox.Visible = false;
            searchbox.ResetText();
            downupkey = false;
        }
        int muv_down_up = 0;
        int prev_muv_down_up = -1;
        bool downupkey = false,down_up_never_done_yet=false;
        bool prevdirection = false;//down
        bool click_instead_of_enter = false;
        private void searchbox_KeyDown_1(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Right))
            {
            }
            if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down))
            {
                
                down_up_never_done_yet = true;
                downupkey = true;
                if(predictionbox.Visible == false)//no up-down will
                    return;//occur when prediction table is not there 
            }
            if((e.KeyCode==Keys.Back)&&(sear!=""))
            {
                //string save = sear;
                char[] seararray = new char[sear.Length];
                seararray = sear.ToCharArray();
                seararray[sear.Length-1] = '\0';
                sear = "";
                int i=0;
                while (seararray[i] != '\0')
                {
                   sear += seararray[i].ToString();
                   i++;
                }
                bool prevbool = correct_or_incorrect.Last();
                correct_or_incorrect.Remove(prevbool);
                bool superprev=false;
                if (correct_or_incorrect.Count > 0)
                    superprev = correct_or_incorrect.Last();
                if ((prevbool == true)||((prevbool==false)&&(superprev==true)))
                {

                   incorrect_input = 0;
                   if (list_of_nexts.Count <= 0)//backspace deleted all the searchbox text
                   {
                       label1.Visible = false;
                       refresh_for_next_word();
                       predictionbox.Visible = false;
                       return;
                   }
                   if(prevbool == true)
                   list_of_nexts.Remove(list_of_nexts.Last());
                   if (list_of_nexts.Count <= 0)//backspace deleted all the searchbox text
                   {
                       label1.Visible = false;
                       refresh_for_next_word();
                       predictionbox.Visible = false;
                       return;
                   }
                   node prevnext = list_of_nexts.Last();
                   next = prevnext;
                   list_of_nexts.Remove(prevnext);
                   cont = true;
                   if (predictionbox.Visible == false)
                       predictionbox.Visible = true;
                   inputbeforEnter(e);
                }
               return;
            }//BACKSPACE PRESSEED
            
            if ((e.KeyCode == Keys.Down) && (muv_down_up<predictionbox.Items.Count))
            {
                if(prev_muv_down_up == -1)//down key pressed for the first time
                    if (muv_down_up < predictionbox.Items.Count)
                        predictionbox.SelectedIndex = muv_down_up++;
                if (muv_down_up < predictionbox.Items.Count)
                {
                    if (prevdirection == true)
                        predictionbox.SelectedIndex = muv_down_up++;
                    if (muv_down_up < predictionbox.Items.Count)
                    predictionbox.SelectedIndex = muv_down_up++;
                }
                prevdirection = false;
            }
            if ((e.KeyCode == Keys.Up) && (muv_down_up > 0))
            {
                downupkey = true;
                if (muv_down_up > 0)
                {
                    if (prevdirection == false)
                        predictionbox.SelectedIndex = --muv_down_up;
                    if (muv_down_up > 0)
                    predictionbox.SelectedIndex = --muv_down_up;
                }
                prevdirection = true;
            }//UP DOWN ARROW KEY
            char c = ((char)(e.KeyValue + 32)).ToString().ToCharArray()[0];
            if (((c < 'a') || (c > 'z')) && (e.KeyCode != Keys.Enter) && (e.KeyCode != Keys.Up) && (e.KeyCode != Keys.Down))
            {
                return;
            }
            if (Mean_box.Visible == true)
            {
                Mean_box.Visible = false;
            }
            if (label1.Visible == false)
                label1.Visible = true;
            if ((predictionbox.Visible == false) && (cont == true))
                predictionbox.Visible = true;
            
            if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down))
            {
                prev_muv_down_up = muv_down_up;
                return;
            }//setting visibles of toolboxes

            if ((downupkey == false) && (click_instead_of_enter == false))
            {
                inputbeforEnter(e);
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (downupkey == true)
                {
                    downupkey = false;
                    item_selected();
                    refresh_for_next_word();
                }
                else
                {
                    searchbox.ResetText();
                    inputafterenter();
                    refresh_for_next_word();
                }
             }
            
        }
        private void predictionbox_MouseClick(object sender, MouseEventArgs e)
        {
            click_instead_of_enter = true;
            item_selected();
            refresh_for_next_word();
            predictionbox.Visible = false;
        }
        private void predictionbox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void gobutton_Click(object sender, EventArgs e)
        {
            if (searchbox.Text == "")
            {
                predictionbox.Visible = false;
                label1.Visible = false;
                Mean_box.Visible = false;
                refresh_for_next_word();
                return;
            }
            click_instead_of_enter = true;
            if (sear == "")
                return;
            searchbox.Text = "";
            if (down_up_never_done_yet == false)
            {
                if (cont == false) //wrong spelling entered
                {
                    predictionbox.Visible = true;
                    predictionbox.ResetText();
                    
                    did_u_mean(distinct, sear);
                }
                else
                    inputafterenter();
                return;
            }
            item_selected();
            refresh_for_next_word();
            predictionbox.Visible = false;
            
        }
        private void searchbox_MouseClick(object sender, MouseEventArgs e)
        {
            if (searchbox.Text == "")
            {
                predictionbox.Visible = false;
                label1.Visible = false;
                Mean_box.Visible = false;
                refresh_for_next_word();
            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
            
            player.URL = project_path+ "\\audio\\" + word_searched+".mp3";
            player.controls.play();
        } 
    }
}    