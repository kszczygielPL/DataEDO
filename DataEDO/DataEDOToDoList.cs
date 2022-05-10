using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DataEDO.DataSave;
using DataEDO.Model.Todo;

namespace DataEDO
{
    public partial class DataEDOToDoList : Form
    {
        Dictionary<FormStatuses, Action> formStatuses = new Dictionary<FormStatuses, Action>();
        FormStatuses currentFormStatus = FormStatuses.DefaultNoItems;

        //default database
        IDataStore dataStore = new DatabaseLink();

        public DataEDOToDoList()
        {
            InitializeComponent();
        }

        private void DataEDOToDoList_Load(object sender, EventArgs e)
        {
            SetActionsForFormStats();
            SetStartupFormStatus();            
        }


        private void SBAdd_Click(object sender, EventArgs e)
        {
            if(currentFormStatus== FormStatuses.DefaultNoItems)
                toDoBindingSource.DataSource = new List<ToDo>();

            SetCurrentFormStatus(FormStatuses.Adding);

            toDoBindingSource.AddNew();
            toDoBindingSource.MoveLast();
            ((ToDo)toDoBindingSource.Current).IsNew = true;

            TitleTextEdit.Focus();
        }
      
        private void SBEdit_Click(object sender, EventArgs e)
        {
            SetCurrentFormStatus(FormStatuses.Editing);
        }

        private void SBCancel_Click(object sender, EventArgs e)
        {
            toDoBindingSource.CancelEdit();
            SetStartupFormStatus();
        }

        private void SBSave_Click(object sender, EventArgs e)
        {
            toDoBindingSource.EndEdit();

            SetStartupFormStatus();

            if(toDoBindingSource.DataSource != null)
            {
                SBLoadToDoList.Enabled = ((List<ToDo>)toDoBindingSource.DataSource).Any(x => x.IsNew);
            }
        }

        private void TitleTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void DescriptionMemoEdit_EditValueChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        

        private void SBLoadToDoList_Click(object sender, EventArgs e)
        {
            //Empty list load from source
            if (currentFormStatus == FormStatuses.DefaultNoItems)
            {
                SelectDataSourceClass();
                LoadToDosFromSource();
                SetStartupFormStatus();
            }

            //List and connection string not empty save data
            if((currentFormStatus == FormStatuses.DefaultPresentItems)&&
                (teConnectionString.Text != String.Empty))
            {
                bool someDataToSave = ((List<ToDo>)toDoBindingSource.DataSource).Any(x => x.IsNew);
                if (someDataToSave)
                    dataStore.SaveToDoList((List<ToDo>)toDoBindingSource.DataSource, teConnectionString.Text);
            }
        }

       

        private void cbeTypeOfConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbeTypeOfConnection.SelectedIndex)
            {
                case 0:
                    sbCreateFile.Enabled = false;
                    sbSetFileWithData.Enabled = false;
                    teConnectionString.Text = "Server= localhost; Database= master; Integrated Security=True;";


                    break;
                case 1:
                    sbCreateFile.Enabled = true;
                    sbSetFileWithData.Enabled = true;
                    teConnectionString.Text = String.Empty;

                    if(String.IsNullOrEmpty(teConnectionString.Text))
                        SBLoadToDoList.Enabled = false;


                    break;
                default:
                    break;
            }
        }

        private void sbSetFileWithData_Click(object sender, EventArgs e)
        {
            if (xoflOpenFile.ShowDialog() == DialogResult.OK)
            {
                teConnectionString.Text = xoflOpenFile.FileName;
            }
            else
            {
                teConnectionString.Text = String.Empty;
            }
        }

        private void sbCreateFile_Click(object sender, EventArgs e)
        {
            if (xsfdNewFile.ShowDialog() == DialogResult.OK)
            {
                teConnectionString.Text = xsfdNewFile.FileName;
                SBLoadToDoList.Enabled=true;
            }
            else
            {
                teConnectionString.Text = String.Empty;
                SBLoadToDoList.Enabled = false;
            }
        }

        private void teConnectionString_EditValueChanged(object sender, EventArgs e)
        {
            switch (cbeTypeOfConnection.SelectedIndex)
            {
                case 0:
                    SBLoadToDoList.Enabled = dataStore.CheckConnectionLink(teConnectionString.Text);

                    break;
                case 1:
                    if(teConnectionString.Text != String.Empty)
                    {
                        if(Path.IsPathFullyQualified( teConnectionString.Text))
                        {
                            if(File.Exists(teConnectionString.Text))
                            {
                                SBLoadToDoList.Enabled = true;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void sbSearchInTitle_Click(object sender, EventArgs e)
        {
            if (teSearchInTitle.Text != String.Empty &&
                    teConnectionString.Text != String.Empty)
            {
                toDoBindingSource.Clear();
                toDoBindingSource.DataSource = dataStore.LoadToDoListWhereTitle(teSearchInTitle.Text, teConnectionString.Text);
            }
        }

        private void sbSearchInDescription_Click(object sender, EventArgs e)
        {
            if (teSearchInDescription.Text != String.Empty &&
                    teConnectionString.Text != String.Empty)
            {
                toDoBindingSource.Clear();
                toDoBindingSource.DataSource = dataStore.LoadToDoListWhereDescritpion(teSearchInDescription.Text, teConnectionString.Text);
            }
        }

        #region Methods
        /// <summary>
        /// Set startup form status according to list of items
        /// </summary>
        private void SetStartupFormStatus()
        {
            if (toDoBindingSource.List.Count > 0)
                currentFormStatus = FormStatuses.DefaultPresentItems;
            else
                currentFormStatus = FormStatuses.DefaultNoItems;

            formStatuses[currentFormStatus].Invoke();
        }

        /// <summary>
        /// Method to set controls state in according to form state
        /// </summary>
        public void SetActionsForFormStats()
        {
            formStatuses[FormStatuses.DefaultNoItems] = () =>
            {
                TitleTextEdit.Enabled = false;
                DescriptionMemoEdit.Enabled = false;
                DateDateEdit.Enabled = false;

                SBAdd.Enabled = true;
                SBEdit.Enabled = false;
                SBCancel.Enabled = false;
                SBSave.Enabled = false;

                GCToDoList.Enabled = true;

                SBLoadToDoList.Text = "Load data from source.";

                gcDataSourceSelection.Enabled = true;
            };

            formStatuses[FormStatuses.DefaultPresentItems] = () =>
            {
                TitleTextEdit.Enabled = false;
                DescriptionMemoEdit.Enabled = false;
                DateDateEdit.Enabled = false;

                SBAdd.Enabled = true;
                SBEdit.Enabled = true;
                SBCancel.Enabled = false;
                SBSave.Enabled = false;

                GCToDoList.Enabled = true;

                SBLoadToDoList.Text = "Save data to destination.";

                gcDataSourceSelection.Enabled = true;
            };

            formStatuses[FormStatuses.Adding] = () =>
            {
                TitleTextEdit.Enabled = true;
                DescriptionMemoEdit.Enabled = true;
                DateDateEdit.Enabled = true;

                SBAdd.Enabled = false;
                SBEdit.Enabled = false;
                SBCancel.Enabled = true;
                SBSave.Enabled = false;

                GCToDoList.Enabled = false;

                SBLoadToDoList.Enabled = false;

                gcDataSourceSelection.Enabled = false;

                ValidateForm();
            };

            formStatuses[FormStatuses.Editing] = () =>
            {
                TitleTextEdit.Enabled = true;
                DescriptionMemoEdit.Enabled = true;
                DateDateEdit.Enabled = true;

                SBAdd.Enabled = false;
                SBEdit.Enabled = false;
                SBCancel.Enabled = true;
                SBSave.Enabled = false;

                GCToDoList.Enabled = false;

                SBLoadToDoList.Enabled = false;

                gcDataSourceSelection.Enabled = false;

                ValidateForm();
            };
        }
        public void SetCurrentFormStatus(FormStatuses formStatus)
        {
            currentFormStatus = formStatus;
            formStatuses[currentFormStatus].Invoke();
        }

        private void ValidateForm()
        {
            EPToDoValidate.ClearErrors();

            if (currentFormStatus == FormStatuses.Adding ||
                 currentFormStatus == FormStatuses.Editing)
            {
                if (String.IsNullOrEmpty(TitleTextEdit.Text))
                {
                    EPToDoValidate.SetError(TitleTextEdit, "Field must be filled.");
                }

                if (String.IsNullOrEmpty(DescriptionMemoEdit.Text))
                {
                    EPToDoValidate.SetError(DescriptionMemoEdit, "Field must be filled.");
                }

                SBSave.Enabled = !EPToDoValidate.HasErrors;
            }
            else
            {
                SBSave.Enabled = false;
            }



        }


        private void SelectDataSourceClass()
        {
            if (teConnectionString.Text != String.Empty)
            {
                switch (cbeTypeOfConnection.SelectedIndex)
                {
                    case 0:
                        dataStore = new DatabaseLink();
                        break;
                    case 1:
                        dataStore = new FileLink();
                        break;
                }
            }
        }
        private void LoadToDosFromSource()
        {
            switch (currentFormStatus)
            {
                case FormStatuses.DefaultNoItems:
                    toDoBindingSource.DataSource = dataStore.LoadToDoList(teConnectionString.Text);
                    break;
                case FormStatuses.DefaultPresentItems:
                    dataStore.SaveToDoList((List<ToDo>)toDoBindingSource.DataSource, teConnectionString.Text);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
