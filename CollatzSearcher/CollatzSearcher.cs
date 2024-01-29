using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollatzSearcher
{
    public class CollatzSearcher
    {
        //how to memorize the patterns found and execlude them quickly??
        //a loop may make a worse result//
        //I can't make a 2^68 length array//
        //maybe I build a dictionary and conitnously update it
        //instead of making divisions I make simple equal comparison
        //it won't work//
        //looping over all the array may make it even worse when_ 
        //the list gets longer//
        //I can sort the array based on how long the pattern is
        //I may use half search method
        public List<NumberPatternCombo> numberPatternCombos = new List<NumberPatternCombo>();
        public ulong fastpower(ulong base1, long power)
        {
            ulong ans = 1;
            for(int i=0;i< power;i++)
            {
              ans*=base1;
            }
            return ans;
        }
        //public Dictionary<ulong, Numpatternanddecrement> numsfound = new Dictionary<ulong,Numpatternanddecrement>();
        public Dictionary<ulong, ulong> numsfound = new Dictionary<ulong,ulong>();
        //bool check2(ulong number,ulong basenum)
        //{
        //    if (numsfound.ContainsKey(number))
        //    {
        //        var patternL = numsfound[number];
        //        if (number==basenum|| patternL.newnum < basenum)
        //        {
        //            if (number != basenum)
        //            {
        //                return true;
        //            }
        //            ulong xnum = number + patternL.pattern;
        //            patternL.newnum = xnum*patternL.factor;
        //            numsfound.Remove(number);
        //            numsfound.Add(xnum, patternL);
        //            return true;
        //        }
        //    }
        //    return false;
            
        //}
        bool check(ulong number)
        {
            if (numsfound.ContainsKey(number))
            {
                var patternL = numsfound[number];
                ulong xnum = number + patternL;
                numsfound.Remove(number);
                //if (patternL != 0)
                //{
                //    if (numsfound.ContainsKey(xnum))
                //    {
                //        numsfound.Remove(xnum);
                //    }
                    numsfound.Add(xnum, patternL);
                //}
                return true;

            }
            return false;

        }
        //public ulong reached = 0;
        public ulong longest = 0;
        public ulong longestnum = 0;
        //public ulong brokes=0;
        //public ulong[] simplelist = new ulong[100000];
        //public ulong[] patterns = new ulong[100000];
       
        bool simpleCheck(ulong num, ulong[] simplelist, ulong[] patterns,int pointwhere)
        {
            for(int i=0;i< pointwhere;i++)
            {
                if (simplelist[i] == num)
                {
                    simplelist[i] += patterns[i];
                    return true;
                }
            }
            return false;
        }
        ulong[] allnumbers = new ulong[100];
        ulong[] slices_starts;
        ulong[] slices_ends;
        public ulong Reached()
        {
            ulong re = 0;
            try
            {
                for (int i = 0; i < slices_starts.Length; i++)
                {
                    if (allnumbers[i] != 0)
                    {
                        re += allnumbers[i] - slices_starts[i];
                    }
                }
            }catch (Exception e) { }
            return re;
        }
        public void Search(ulong start,ulong end)
        {
            Console.WriteLine("Search started from " + start + " to " + end);
            ulong log10 = (ulong)Math.Log2(end) + 1;
            log10 = 20;
            if (start % 2 == 0)
            {
                start = start + 1;
            }
            //ulong first = 1;
            
            //first = 1;
            int parallel = 9;
            slices_starts = new ulong[parallel];
            slices_ends = new ulong[parallel];
            ulong parallelstep=(end-start)/(ulong)parallel;
            ulong firstf = start;
            List<ulong[]> lists=new List<ulong[]>();
            List<ulong[]> lists2=new List<ulong[]>();
            for(int i=0; i<parallel; i++)
            {
                slices_starts[i] = firstf;
                firstf += parallelstep;
                slices_ends[i] = firstf;
                lists.Add(new ulong[1000]);
                lists2.Add(new ulong[1000]);
            }
            Parallel.For(0, parallel, i =>
            {
                ulong st = slices_starts[i];
                ulong nd= slices_ends[i];
                ulong localfirst = 0;
                ulong[] simplelist = lists[i];
                ulong[] patterns = lists2[i];
                int pointwhere = 0;
                ulong wait = 2;
                ulong next = 0;
                if (st % 2 == 0)
                {
                    st += 1;
                }
                int s16 = 0;
                int s32 = 0;
                for (ulong num = st; num <= st+ 1000000; num += 2)
                {
                    if (simpleCheck(num, simplelist, patterns, pointwhere))// || check(num))
                    {
                        allnumbers[i] = num;
                        continue;
                    }
                    ulong Pnum = num;
                    ulong step = 0;
                    ulong pattern = 1;
                    while (Pnum > num || step == 0)
                    {
                        if (Pnum % 2 == 0)
                        {
                            Pnum /= 2;//z division
                        }
                        else
                        {
                            Pnum = (Pnum * 3 + 1) / 2;//n increment
                                                      //if (!numsfound.ContainsKey(Pnum))
                                                      //{
                                                      //    numsfound.Add(Pnum, 0);
                                                      //}
                        }
                        //if (check(Pnum, num))
                        //{
                        //    broke = true;
                        //    brokes++;
                        //    break;
                        //}
                        pattern *= 2;
                        step++;
                    }
                    if (step == 2&&localfirst==0)
                    {
                        localfirst = num;
                    }
                    ulong pp = num + pattern;
                    if (pp < end&&step>2)
                    {
                        //Numpatternanddecrement PatternL = new Numpatternanddecrement();
                        //PatternL.pattern = pattern;
                        //PatternL.factor = factor;
                        //PatternL.newnum = pp * factor;
                        //numsfound.TryAdd(pp, PatternL);
                        //numsfound.Add(pp, pattern);
                        if (pointwhere < 250 && step < 8)
                        {
                            
                            if ((!(pattern==16&&s16>=1)&&!(pattern==32&&s32>=2))&&(step<8&&s16>=1&&s32>=2||pattern<33))
                            {
                                if (st == 1)
                                {

                                }
                                simplelist[pointwhere] = pp;
                                patterns[pointwhere] = pattern;
                                pointwhere++;
                            }
                            else if (num >= wait)
                            {
                                wait *= 2;
                                next++;
                            }
                            if (pattern == 16)
                            {
                                s16++;
                            }
                            else if (pattern == 32)
                            {
                                s32++;
                            }
                        }
                        else
                        {
                            // numsfound.Add(pp, pattern);
                        }
                    }
                }
                if (st == 1)
                {

                }
                for (ulong num = st; num <= nd; num += 2)
                {
                    if (num == localfirst)
                    {
                        allnumbers[i] = num;
                        localfirst += 4;
                        continue;
                    }
                    if (simpleCheck(num,simplelist,patterns,pointwhere))// || check(num))
                    {
                        allnumbers[i] = num;
                        continue;
                    }
                    ulong Pnum = num;
                    ulong step = 0;
                    while (Pnum > num || step == 0)
                    {
                        if (Pnum % 2 == 0)
                        {
                            Pnum /= 2;//z division
                        }
                        else
                        {
                            Pnum = (Pnum * 3 + 1) / 2;//n increment
                                                      //if (!numsfound.ContainsKey(Pnum))
                                                      //{
                                                      //    numsfound.Add(Pnum, 0);
                                                      //}
                        }
                        //if (check(Pnum, num))
                        //{
                        //    broke = true;
                        //    brokes++;
                        //    break;
                        //}
                        step++;
                    }
                    if (step > longest)
                    {
                        longest = step;
                        longestnum = num;
                    }

                    //NumberPatternCombo numberPatternCombo = new NumberPatternCombo();
                    //numberPatternCombo.Number = num;
                    //numberPatternCombo.Pattern = pattern;
                    //numberPatternCombos.Add(numberPatternCombo);
                    //numberPatternCombos.Sort();
                    //reached = num;
                    //Interlocked.Increment(ref reached);
                    allnumbers[i] = num;
                }
            });
           
        }
    }

    public struct Numpatternanddecrement
    {
        public ulong pattern;
        public decimal factor;
        public decimal newnum;
        public override string ToString()
        {
            return $"Pattern: {pattern}, Factor: {factor}, Newnum:{newnum}";
        }
    }
    public class NumberPatternCombo : IComparable<NumberPatternCombo>
    {
        public ulong Number;
        public ulong Pattern;
        public  int CompareTo(NumberPatternCombo? x)
        {
            return Pattern.CompareTo(x.Pattern);
        }
    }
    
}
