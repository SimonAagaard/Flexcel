﻿using System;
using System.ComponentModel;
using System.Windows;
using Domain;
using System.Linq;
using System.Collections.Generic;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FrontEnd : Window
    {
        MainWindow mainWindow = new MainWindow();

        public FrontEnd()
        {
            InitializeComponent();
        }

        private void BtnMasterDataFilePathSelect_Click(object sender, RoutedEventArgs e)
        {
            txtBoxFilePathMasterData.Text = mainWindow.ChooseCSVFile();
        }
        private void BtnRouteNumberFilePathSelect_Click(object sender, RoutedEventArgs e)
        {
            txtBoxFilePathRouteNumberOffer.Text = mainWindow.ChooseCSVFile();
        }
        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtBoxFilePathMasterData.Text.ToString().Equals("") || txtBoxFilePathRouteNumberOffer.Text.ToString().Equals(""))
                {
                    MessageBox.Show("Vælg venligst begge filer inden import startes");
                }
                else if ((txtBoxFilePathMasterData.Text.ToString().Equals("") && txtBoxFilePathRouteNumberOffer.Text.ToString().Equals("")))
                {
                    MessageBox.Show("Vælg venligst filerne inden import startes");
                }
                else
                {
                    mainWindow.ImportCSV(txtBoxFilePathMasterData.Text.ToString(), txtBoxFilePathRouteNumberOffer.Text.ToString());
                    MessageBox.Show("Filerne er nu importeret");
                    mainWindow.ImportDone = true;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

        }
        private void BtnStartSelection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainWindow.InitializeSelection();
                ListContainer listContainer = ListContainer.GetInstance();
                List<Offer> outputListByUserID = listContainer.outputList.OrderBy(x => x.UserID).ToList();
                listView.ItemsSource = outputListByUserID;
                foreach(Offer offer in listContainer.outputList)
                {
                    offer.Contractor.CountNumberOfWonOffersOfEachType(listContainer.outputList); 
                }
                MessageBox.Show("Udvælgelsen er nu færdig");
            }
            catch (Exception x)
            {
                PromptWindow promptWindow = new PromptWindow(x.Message);
                promptWindow.Show();
            }
        }
      
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Environment.Exit(1);
        }

        private void btnSavePublic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainWindow.SaveCSVPublishFile();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void btnSaveCall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainWindow.SaveCSVCallFile();
            }          
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}