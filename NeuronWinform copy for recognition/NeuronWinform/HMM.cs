using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Fields;
using Accord.Statistics.Models.Fields.Functions;
using Accord.Statistics.Models.Fields.Learning;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov.Topology;
using Gestures.HMMs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
namespace NeuronWinform
{
    public class HMM
    {
        public MainWindow mainwindow;

        public HiddenMarkovClassifier<MultivariateNormalDistribution> hmm;
        public HiddenConditionalRandomField<double[]> hcrf;

        public string LearnHMM()
        {
            try
            {
                BindingList<Sequence> samples = mainwindow.database.Samples;
                BindingList<String> classes = mainwindow.database.Classes;

                double[][][] inputs = new double[samples.Count][][];
                int[] outputs = new int[samples.Count];

                for (int i = 0; i < inputs.Length; i++)
                {
                    inputs[i] = samples[i].Input;
                    outputs[i] = samples[i].Output;
                }

                int states = 5;
                int iterations = 0;
                double tolerance = 0.01;
                bool rejection = false;


                hmm = new HiddenMarkovClassifier<MultivariateNormalDistribution>(classes.Count,
                    new Forward(states), new MultivariateNormalDistribution(3), classes.ToArray());


                // Create the learning algorithm for the ensemble classifier
                var teacher = new HiddenMarkovClassifierLearning<MultivariateNormalDistribution>(hmm,

                    // Train each model using the selected convergence criteria
                    i => new BaumWelchLearning<MultivariateNormalDistribution>(hmm.Models[i])
                    {
                        Tolerance = tolerance,
                        Iterations = iterations,

                        FittingOptions = new NormalOptions()
                        {
                            Regularization = 1e-5
                        }
                    }
                );

                teacher.Empirical = true;
                teacher.Rejection = rejection;


                // Run the learning algorithm
                double error = teacher.Run(inputs, outputs);


                // Classify all training instances
                foreach (var sample in mainwindow.database.Samples)
                {
                    sample.RecognizedAs = hmm.Compute(sample.Input);
                }

                foreach (DataGridViewRow row in mainwindow.gridSamples.Rows)
                {
                    var sample = row.DataBoundItem as Sequence;
                    row.DefaultCellStyle.BackColor = (sample.RecognizedAs == sample.Output) ?
                        Color.LightGreen : Color.White;
                }

                return "Ready!";
            }
            catch (Exception ex)
            {
                DumpException(ex);
                return "!HMM Error!";
            }
        }

        public void LeardHCRF()
        {
            var samples = mainwindow.database.Samples;
            var classes = mainwindow.database.Classes;

            double[][][] inputs = new double[samples.Count][][];
            int[] outputs = new int[samples.Count];

            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = samples[i].Input;
                outputs[i] = samples[i].Output;
            }

            int iterations = 100;
            double tolerance = 0.01;


            hcrf = new HiddenConditionalRandomField<double[]>(
                new MarkovMultivariateFunction(hmm));


            // Create the learning algorithm for the ensemble classifier
            var teacher = new HiddenResilientGradientLearning<double[]>(hcrf)
            {
                Iterations = iterations,
                Tolerance = tolerance
            };


            // Run the learning algorithm
            double error = teacher.Run(inputs, outputs);


            foreach (var sample in mainwindow.database.Samples)
            {
                sample.RecognizedAs = hcrf.Compute(sample.Input);
            }
        }

        public void DumpException(Exception ex)
        {
            Console.WriteLine("--------- Outer Exception Data ---------");
            WriteExceptionInfo(ex);
            ex = ex.InnerException;
            if (null != ex)
            {
                Console.WriteLine("--------- Inner Exception Data ---------");
                WriteExceptionInfo(ex.InnerException);
                ex = ex.InnerException;
            }
        }
        public void WriteExceptionInfo(Exception ex)
        {
            Console.WriteLine("Message: {0}", ex.Message);
            Console.WriteLine("Exception Type: {0}", ex.GetType().FullName);
            Console.WriteLine("Source: {0}", ex.Source);
            Console.WriteLine("StrackTrace: {0}", ex.StackTrace);
            Console.WriteLine("TargetSite: {0}", ex.TargetSite);
        }
    }
}
