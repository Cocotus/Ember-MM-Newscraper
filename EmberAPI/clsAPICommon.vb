﻿' ################################################################################
' #                             EMBER MEDIA MANAGER                              #
' ################################################################################
' ################################################################################
' # This file is part of Ember Media Manager.                                    #
' #                                                                              #
' # Ember Media Manager is free software: you can redistribute it and/or modify  #
' # it under the terms of the GNU General Public License as published by         #
' # the Free Software Foundation, either version 3 of the License, or            #
' # (at your option) any later version.                                          #
' #                                                                              #
' # Ember Media Manager is distributed in the hope that it will be useful,       #
' # but WITHOUT ANY WARRANTY; without even the implied warranty of               #
' # MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                #
' # GNU General Public License for more details.                                 #
' #                                                                              #
' # You should have received a copy of the GNU General Public License            #
' # along with Ember Media Manager.  If not, see <http://www.gnu.org/licenses/>. #
' ################################################################################

Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports System.Drawing
Imports System.Windows.Forms
Imports NLog

Public Class Containers

#Region "Nested Types"

    <XmlType(AnonymousType:=True),
     XmlRoot([Namespace]:="", IsNullable:=False, ElementName:="CommandFile")>
    Public Class InstallCommands

#Region "Properties"

        <XmlElement("noTransaction")>
        Public Property noTransaction() As List(Of CommandsNoTransactionCommand) = New List(Of CommandsNoTransactionCommand)

        <XmlElement("transaction")>
        Public Property transaction() As List(Of CommandsTransaction) = New List(Of CommandsTransaction)

#End Region 'Properties

#Region "Methods"

        Public Shared Function Load(ByVal fpath As String) As InstallCommands
            If Not File.Exists(fpath) Then Return New InstallCommands
            Dim xmlSer As XmlSerializer
            xmlSer = New XmlSerializer(GetType(InstallCommands))
            Using xmlSW As New StreamReader(fpath)
                Return DirectCast(xmlSer.Deserialize(xmlSW), InstallCommands)
            End Using
        End Function

        Public Sub Save(ByVal fpath As String)
            Dim xmlSer As New XmlSerializer(GetType(InstallCommands))
            Using xmlSW As New StreamWriter(fpath)
                xmlSer.Serialize(xmlSW, Me)
            End Using
        End Sub

#End Region 'Methods

    End Class

    <XmlType(AnonymousType:=True)>
    Partial Public Class CommandsTransaction

#Region "Properties"

        <XmlElement("command")>
        Public Property command() As List(Of CommandsTransactionCommand) = New List(Of CommandsTransactionCommand)

        <XmlAttribute()>
        Public Property name() As String = String.Empty

#End Region 'Properties 

    End Class

    <XmlType(AnonymousType:=True)>
    Partial Public Class CommandsTransactionCommand

#Region "Properties"

        Public Property description() As String = String.Empty

        Public Property execute() As String = String.Empty

        <XmlAttribute()>
        Public Property type() As String = String.Empty

#End Region 'Properties 

    End Class

    <XmlType(AnonymousType:=True)>
    Partial Public Class CommandsNoTransactionCommand

#Region "Properties"

        Public Property description() As String = String.Empty

        Public Property execute() As String = String.Empty

        <XmlAttribute()>
        Public Property type() As String = String.Empty

#End Region 'Properties

    End Class

    Public Class ImgResult

#Region "Properties"

        Public Property Fanart() As MediaContainers.Fanart = New MediaContainers.Fanart

        Public Property ImagePath() As String = String.Empty

        Public Property Posters() As List(Of String) = New List(Of String)

#End Region 'Properties 

    End Class

    Public Class SettingsPanel

#Region "Fields"

        Dim _imageindex As Integer
        Dim _image As Image
        Dim _name As String
        Dim _order As Integer
        Dim _panel As Panel
        Dim _parent As String
        Dim _prefix As String
        Dim _text As String
        Dim _type As String

#End Region 'Fields

#Region "Constructors"
        ''' <summary>
        ''' Overload the default New() method to provide proper initialization of fields
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property ImageIndex() As Integer
            Get
                Return _imageindex
            End Get
            Set(ByVal value As Integer)
                _imageindex = value
            End Set
        End Property

        <XmlIgnore()>
        Public Property Image() As Image
            Get
                Return _image
            End Get
            Set(ByVal value As Image)
                _image = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Property Order() As Integer
            Get
                Return _order
            End Get
            Set(ByVal value As Integer)
                _order = value
            End Set
        End Property

        <XmlIgnore()>
        Public Property Panel() As Panel
            Get
                Return _panel
            End Get
            Set(ByVal value As Panel)
                _panel = value
            End Set
        End Property

        Public Property Parent() As String
            Get
                Return _parent
            End Get
            Set(ByVal value As String)
                _parent = value
            End Set
        End Property

        Public Property Prefix() As String
            Get
                Return _prefix
            End Get
            Set(ByVal value As String)
                _prefix = value
            End Set
        End Property

        Public Property Text() As String
            Get
                Return _text
            End Get
            Set(ByVal value As String)
                _text = value
            End Set
        End Property

        Public Property Type() As String
            Get
                Return _type
            End Get
            Set(ByVal value As String)
                _type = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _imageindex = 0
            _image = Nothing
            _name = String.Empty
            _order = 0
            _panel = New Panel
            _parent = String.Empty
            _prefix = String.Empty
            _text = String.Empty
            _type = String.Empty
        End Sub

#End Region 'Methods

    End Class

    Public Class Addon

#Region "Fields"
        Private _id As Integer
        Private _name As String
        Private _author As String
        Private _description As String
        Private _category As String
        Private _version As Single
        Private _mineversion As Single
        Private _maxeversion As Single
        Private _screenshotpath As String
        Private _screenshotimage As Image
        Private _files As SortedList(Of String, String)
        Private _deletefiles As List(Of String)
#End Region 'Fields

#Region "Properties"
        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Property Author() As String
            Get
                Return _author
            End Get
            Set(ByVal value As String)
                _author = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Public Property Category() As String
            Get
                Return _category
            End Get
            Set(ByVal value As String)
                _category = value
            End Set
        End Property

        Public Property Version() As Single
            Get
                Return _version
            End Get
            Set(ByVal value As Single)
                _version = value
            End Set
        End Property

        Public Property MinEVersion() As Single
            Get
                Return _mineversion
            End Get
            Set(ByVal value As Single)
                _mineversion = value
            End Set
        End Property

        Public Property MaxEVersion() As Single
            Get
                Return _maxeversion
            End Get
            Set(ByVal value As Single)
                _maxeversion = value
            End Set
        End Property

        Public Property ScreenShotPath() As String
            Get
                Return _screenshotpath
            End Get
            Set(ByVal value As String)
                _screenshotpath = value
            End Set
        End Property

        Public Property ScreenShotImage() As Image
            Get
                Return _screenshotimage
            End Get
            Set(ByVal value As Image)
                _screenshotimage = value
            End Set
        End Property

        Public Property Files() As SortedList(Of String, String)
            Get
                Return _files
            End Get
            Set(ByVal value As SortedList(Of String, String))
                _files = value
            End Set
        End Property

        Public Property DeleteFiles() As List(Of String)
            Get
                Return _deletefiles
            End Get
            Set(ByVal value As List(Of String))
                _deletefiles = value
            End Set
        End Property
#End Region 'Properties

#Region "Constructors"
        Public Sub New()
            Clear()
        End Sub
#End Region 'Constructors

#Region "Methods"
        Public Sub Clear()
            _id = -1
            _name = String.Empty
            _author = String.Empty
            _description = String.Empty
            _category = String.Empty
            _version = -1
            _mineversion = -1
            _maxeversion = -1
            _screenshotpath = String.Empty
            _screenshotimage = Nothing
            _files = New SortedList(Of String, String)
            _deletefiles = New List(Of String)
        End Sub
#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class

#Region "Enumerations"

Public Class Enums

    Public Enum ContentType As Integer
        None = 0
        Generic = 1
        Movie = 2
        Movieset = 3
        MusicVideo = 4
        Person = 5
        TV = 6
        TVEpisode = 7
        TVSeason = 8
        TVShow = 9
    End Enum

    Public Enum DefaultType As Integer
        All
        MainTabSorting
        MovieFilters
        MovieListSorting
        MoviesetListSorting
        MoviesetSortTokens
        MovieSortTokens
        TVEpisodeFilters
        TVEpisodeListSorting
        TVSeasonListSorting
        TVSeasonTitleBlacklist
        TVShowFilters
        TVShowListSorting
        TVShowMatching
        TVShowSortTokens
        TrailerCodec
        ValidExts
        ValidSubtitleExts
        ValidThemeExts
    End Enum
    ''' <summary>
    ''' 0 results in using the current datetime when adding a video
    ''' 1 results in prefering to use the files mtime (if it's valid) and only using the file's ctime if the mtime isn't valid
    ''' 2 results in using the newer datetime of the file's mtime and ctime
    ''' </summary>
    ''' <remarks>Don't remove the enum integer values to keep "Now" as default value</remarks>
    Public Enum DateTime As Integer
        Now = 0
        ctime = 1
        mtime = 2
        Newer = 3
    End Enum
    ''' <summary>
    ''' Enum representing valid TV series ordering.
    ''' </summary>
    ''' <remarks>Don't remove the enum integer values to keep "Standard" as default value</remarks>
    Public Enum EpisodeOrdering As Integer
        Standard = 0
        DVD = 1
        Absolute = 2
        DayOfYear = 3
    End Enum
    ''' <summary>
    ''' Enum representing Order of displaying Episodes
    ''' </summary>
    ''' <remarks>Don't remove the enum integer values to keep "Episode" as default value</remarks>
    Public Enum EpisodeSorting As Integer
        Episode = 0
        Aired = 1
    End Enum
    ''' <summary>
    ''' Known image sizes
    ''' </summary>
    ''' <remarks>Don't remove the enum integer values to keep the quality sorting</remarks>
    Public Enum ImageSize As Integer
        ''' <summary>
        ''' Movie Poster 2000x3000 / TVShow Poster 2000x3000
        ''' </summary>
        HD3000 = 0
        ''' <summary>
        ''' Movie Fanart 3840x2160 / Episode Poster 3840x2160 / TVShow Fanart 3840x2160
        ''' </summary>
        UHD2160 = 1
        ''' <summary>
        ''' Movie Poster 1400x2100
        ''' </summary>
        HD2100 = 2
        ''' <summary>
        ''' Movie Poster 1000x1500 / TVShow Poster 1000x1500 / Season Poster 1000x1500
        ''' </summary>
        HD1500 = 3
        ''' <summary>
        ''' Movie Fanart 2560x1440 / TVShow Fanart 2560x1440
        ''' </summary>
        QHD1440 = 4
        ''' <summary>
        ''' Movie Poster 1000x1426 / TVShow Poster 1000x1426 / Season Poster 1000x1426
        ''' </summary>
        HD1426 = 5
        ''' <summary>
        ''' Movie Fanart 1920x1080 / Episode Poster 1920x1080 / TVShow Fanart 1920x1080
        ''' </summary>
        HD1080 = 6
        ''' <summary>
        ''' Movie DiscArt 1000x1000 / TVShow Poster 680x1000
        ''' </summary>
        HD1000 = 7
        ''' <summary>
        ''' Movie Fanart 1280x720 / Episode Poster 1280x720 / TVShow Fanart 1280x720
        ''' </summary>
        HD720 = 8
        ''' <summary>
        ''' Season Poster 400x578
        ''' </summary>
        HD578 = 9
        ''' <summary>
        ''' Movie ClearArtHD 1000x562 / Movie Landscape 1000x562 / TVShow ClearArtHD 1000x562 / TVShow Landscape 1000x562
        ''' </summary>
        HD562 = 10
        ''' <summary>
        ''' TV CharacterArt 512x512
        ''' </summary>
        HD512 = 11
        ''' <summary>
        ''' Movie ClearLogoHD 800x310 / TVShow ClearLogoHD 800x310
        ''' </summary>
        HD310 = 12
        ''' <summary>
        ''' Movie ClearArtSD 500x281 / TVShow ClearArtSD 500x281
        ''' </summary>
        SD281 = 13
        ''' <summary>
        ''' Episode Poster 400x300 (400x225 for 16:9 images)
        ''' </summary>
        SD225 = 14
        ''' <summary>
        ''' Movie Banner 1000x185 / TVShow Banner 1000x185
        ''' </summary>        
        HD185 = 15
        ''' <summary>
        ''' Movie ClearLogoSD 400x155 / TVShow ClearLogoSD 400x155
        ''' </summary>
        SD155 = 16
        ''' <summary>
        ''' TVShow Banner 758x140
        ''' </summary>
        HD140 = 17
        ''' <summary>
        ''' Any size that does not correspond to a standard
        ''' </summary>
        Any = 99
    End Enum

    Public Enum ModifierType As Integer
        All
        AllSeasonsBanner
        AllSeasonsFanart
        AllSeasonsLandscape
        AllSeasonsPoster
        DoSearch
        EpisodeActorThumbs
        EpisodeFanart
        EpisodePoster
        EpisodeMeta
        EpisodeNFO
        EpisodeSubtitle
        EpisodeWatchedFile
        MainActorThumbs
        MainBanner
        MainCharacterArt
        MainClearArt
        MainClearLogo
        MainDiscArt
        MainExtrafanarts
        MainExtrathumbs
        MainFanart
        MainKeyArt
        MainLandscape
        MainMeta
        MainNFO
        MainPoster
        MainSubtitle
        MainTheme
        MainTrailer
        MainWatchedFile
        SeasonBanner
        SeasonFanart
        SeasonLandscape
        SeasonNFO
        SeasonPoster
        withEpisodes
        withSeasons
    End Enum

    Public Enum ModuleEventType As Integer
        ''' <summary>
        ''' Called after edit movie
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_Movie
        ''' <summary>
        ''' Called after edit movieset
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_MovieSet
        ''' <summary>
        ''' Called after edit episode
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_TVEpisode
        ''' <summary>
        ''' Called after edit season
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_TVSeason
        ''' <summary>
        ''' Called after edit show
        ''' </summary>
        ''' <remarks></remarks>
        AfterEdit_TVShow
        ''' <summary>
        ''' Called after update DB process
        ''' </summary>
        ''' <remarks></remarks>
        AfterUpdateDB_TV
        ''' <summary>
        ''' Called after update DB process
        ''' </summary>
        ''' <remarks></remarks>
        AfterUpdateDB_Movie
        ''' <summary>
        ''' Called when Manual editing or reading from nfo
        ''' </summary>
        ''' <remarks></remarks>
        BeforeEdit_Movie
        ''' <summary>
        ''' Called when Manual editing or reading from nfo
        ''' </summary>
        ''' <remarks></remarks>
        BeforeEdit_MovieSet
        ''' <summary>
        ''' Called when Manual editing or reading from nfo
        ''' </summary>
        ''' <remarks></remarks>
        BeforeEdit_TVEpisode
        ''' <summary>
        ''' Called when Manual editing or reading from nfo
        ''' </summary>
        ''' <remarks></remarks>
        BeforeEdit_TVSeason
        ''' <summary>
        ''' Called when Manual editing or reading from nfo
        ''' </summary>
        ''' <remarks></remarks>
        BeforeEdit_TVShow
        ''' <summary>
        ''' Command Line Module Call
        ''' </summary>
        ''' <remarks></remarks>
        CommandLine
        Generic
        Notification
        OnBannerSave_Movie
        OnClearArtSave_Movie
        OnClearLogoSave_Movie
        OnDiscArtSave_Movie
        OnFanartDelete_Movie
        OnFanartSave_Movie
        OnLandscapeSave_Movie
        OnNFORead_TVShow
        OnNFOSave_Movie
        OnNFOSave_TVShow
        OnPosterDelete_Movie
        OnPosterSave_Movie
        OnThemeSave_Movie
        OnTrailerSave_Movie
        RandomFrameExtrator
        Remove_Movie
        Remove_MovieSet
        Remove_TVEpisode
        Remove_TVSeason
        Remove_TVShow
        ''' <summary>
        ''' Called during auto scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperMulti_Movie
        ''' <summary>
        ''' Called during auto scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperMulti_TVEpisode
        ''' <summary>
        ''' Called during manual scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperSingle_Movie
        ''' <summary>
        ''' Called during manual scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperSingle_TVEpisode
        ShowMovie
        ShowTVShow
        SyncModuleSettings
        Sync_Movie
        Sync_MovieSet
        Sync_TVEpisode
        Sync_TVSeason
        Sync_TVShow
        Task
        ''' <summary>
        ''' Called during auto scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperMulti_TVShow
        ''' <summary>
        ''' Called during manual scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperSingle_TVShow
        ''' <summary>
        ''' Called during auto scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperMulti_TVSeason
        ''' <summary>
        ''' Called during manual scraping
        ''' </summary>
        ''' <remarks></remarks>
        ScraperSingle_TVSeason
        DuringUpdateDB_TV
    End Enum

    Public Enum ScannerEventType As Integer
        None
        Added_Movie
        Added_TVEpisode
        Added_TVShow
        CleaningDatabase
        CurrentSource
        PreliminaryTasks
        Refresh_TVShow
        ScannerEnded
        ScannerStarted
    End Enum

    Public Enum ScraperEventType As Integer
        BannerItem
        CharacterArtItem
        ClearArtItem
        ClearLogoItem
        DiscArtItem
        ExtrafanartsItem
        ExtrathumbsItem
        FanartItem
        LandscapeItem
        NFOItem
        PosterItem
        ThemeItem
        TrailerItem
    End Enum
    ''' <summary>
    ''' Enum representing which Movies/TVShows should be scraped,
    ''' and whether results should be automatically chosen or asked of the user.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ScrapeType As Integer
        AllAuto = 0
        AllAsk = 1
        AllSkip = 2
        MissingAuto = 3
        MissingAsk = 4
        MissingSkip = 5
        NewAuto = 6
        NewAsk = 7
        NewSkip = 8
        MarkedAuto = 9
        MarkedAsk = 10
        MarkedSkip = 11
        FilterAuto = 12
        FilterAsk = 13
        FilterSkip = 14
        SingleScrape = 15
        SingleField = 16
        SingleAuto = 17
        SelectedAuto = 18
        SelectedAsk = 19
        SelectedSkip = 20
        None = 99
    End Enum

    Public Enum SelectionType As Integer
        All
        Selected
    End Enum
    ''' <summary>
    ''' Movie sort methode inside of a movieset
    ''' </summary>
    ''' <remarks>Don't remove the enum integer values to keep "Year" as default value</remarks>
    Public Enum SortMethod_MovieSet As Integer
        Year = 0    'default in Kodi, so have to be on the first position of enumeration
        Title = 1
    End Enum

    Public Enum TaskManagerEventType As Integer
        RefreshRow
        SimpleMessage
        TaskManagerEnded
        TaskManagerStarted
    End Enum

    Public Enum TaskManagerType As Integer
        ''' <summary>
        ''' only to set nothing after init
        ''' </summary>
        None
        DataFields_ClearOrReplace
        CopyBackdrops
        DoTitleCheck
        GetMissingEpisodes
        Reload
        SetLanguage
        SetLockedState
        SetMarkedState
        ''' <summary>
        ''' Needs MediaContainer.SetDetails or Nothing as GenericObject. Nothing removes all assigned movie sets.
        ''' </summary>
        SetMovieset
        SetWatchedState
    End Enum

    Public Enum TVBannerType As Integer
        Blank = 0       'will leave the title and show logo off the banner
        Graphical = 1   'will show the series name in the show's official font or will display the actual logo for the show
        Text = 2        'will show the series name as plain text in an Arial font
        Any = 99
    End Enum
    ''' <summary>
    ''' Enum representing the trailer codec options
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrailerAudioCodec As Integer
        MP4 = 0
        WebM = 1
        UNKNOWN = 99
    End Enum
    ''' <summary>
    ''' Enum representing the trailer quality options
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrailerAudioQuality As Integer
        AAC256kbps = 0
        AAC128kbps = 1
        AAC48kbps = 2
        Vorbis192kbps = 3
        Vorbis128kbps = 4
        UNKNOWN = 5
        Any = 99
    End Enum
    ''' <summary>
    ''' Enum representing the trailer type options
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrailerType As Integer
        Clip = 0
        Featurette = 1
        Teaser = 2
        Trailer = 3
        Any = 99
    End Enum
    ''' <summary>
    ''' Enum representing the trailer codec options
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrailerVideoCodec As Integer
        MP4 = 0
        WebM = 1
        v3GP = 2
        FLV = 3
        UNKNOWN = 4
    End Enum
    ''' <summary>
    ''' Enum representing the trailer quality options
    ''' </summary>
    ''' <remarks>Don't remove the enum integer values to keep the quality sorting</remarks>
    Public Enum TrailerVideoQuality As Integer
        HD2160p = 0
        HD2160p60fps = 1
        HD1440p = 2
        HD1080p = 3
        HD1080p60fps = 4
        HD720p = 5
        HD720p60fps = 6
        HQ480p = 7 'or 576 for 4:3 media
        SQ360p = 8
        SQ240p = 9 'or 270
        SQ144p = 10
        SQ144p15fps = 11
        UNKNOWN = 12
        Any = 99
    End Enum

#End Region 'Enumerations

End Class 'Enums

Public Class Functions

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

    ''' <summary>
    ''' Gets the base directory that the assembly resolver uses to probe for assemblies (like the current application executable)
    ''' </summary>
    ''' <returns>Path of the directory containing the Ember executable</returns>
    Public Shared Function AppPath() As String
        Return System.AppDomain.CurrentDomain.BaseDirectory
    End Function
    ''' <summary>
    ''' Determine whether we are running a 64-bit instance
    ''' </summary>
    ''' <returns><c>True</c> if we are running a 64-bit instance</returns>
    ''' <remarks>Note that the value of IntPtr.Size is 4 in a 32-bit process, and 8 in a 64-bit process</remarks>
    Public Shared Function Check64Bit() As Boolean
        Return (IntPtr.Size = 8)
    End Function
    ''' <summary>
    ''' Determine whether this instance is intended as a beta test version
    ''' </summary>
    ''' <returns><c>True</c> if this instance is a beta version, <c>False</c> otherwise</returns>
    ''' <remarks>The defining test for being a beta version is the existance of the file "Beta.Tester" in the same directory as
    ''' the Ember executable.</remarks>
    Public Shared Function IsBetaEnabled() As Boolean
        If File.Exists(Path.Combine(AppPath, "Beta.Tester")) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Check for the lastest version of Ember
    ''' </summary>
    ''' <returns>Latest version as integer</returns>
    ''' <remarks>Not implemented yet. This method is currently a stub, and always returns False</remarks>
    Public Shared Function CheckNeedUpdate() As Boolean
        'TODO STUB - Not implemented yet
        Dim needUpdate As Boolean = False
        'Dim sHTTP As New HTTP
        'Dim platform As String = "x86"
        'Dim updateXML As String = sHTTP.DownloadData(String.Format("http://pcjco.dommel.be/emm-r/{0}/versions.xml", If(IsBetaEnabled(), "updatesbeta", "updates")))
        'sHTTP = Nothing
        'If updateXML.Length > 0 Then
        '    For Each v As ModulesManager.VersionItem In ModulesManager.VersionList
        '        Dim vl As ModulesManager.VersionItem = v
        '        Dim n As String = String.Empty
        '        Dim xmlUpdate As XDocument
        '        Try
        '            xmlUpdate = XDocument.Parse(updateXML)
        '        Catch
        '            Return False
        '        End Try
        '        Dim xUdpate = From xUp In xmlUpdate...<Config>...<Modules>...<File> Where (xUp.<Version>.Value <> "" AndAlso xUp.<Name>.Value = vl.AssemblyFileName AndAlso xUp.<Platform>.Value = platform) Select xUp.<Version>.Value
        '        Try
        '            If Convert.ToInt16(xUdpate(0)) > Convert.ToInt16(v.Version) Then
        '                v.NeedUpdate = True
        '                needUpdate = True
        '            End If
        '        Catch ex As Exception
        '        End Try
        '    Next
        'End If
        Return needUpdate
    End Function
    ''' <summary>
    ''' Convert a Unix Timestamp to a VB DateTime
    ''' </summary>
    ''' <param name="timestamp">A valid unix-style timestamp</param>
    ''' <returns>An appropriately formatted DateTime representing the supplied timestamp</returns>
    ''' <remarks></remarks>
    ''' <exception cref="ArgumentException"> thrown when <paramref name="timestamp"/> is <c>Double.NAN</c>.</exception>
    ''' <exception cref="ArgumentOutOfRangeException"> thrown when <paramref name="timestamp"/> is <c>Double.NegativeInfinity</c> or <c>Double.PositiveInfinity</c>,
    ''' or if resulting <c>DateTime</c> would be outside the bounds of Jan 1, 0001 and Dec 31, 9999</exception>
    Public Shared Function ConvertFromUnixTimestamp(ByVal timestamp As Double) As DateTime
        If timestamp.CompareTo(Double.NaN) = 0 Then
            Throw New ArgumentException("Parameter was not a number (Double.NAN)", "timestamp")
        End If
        'Input can not be: NaN (not a number), Positive Infinity, Negative Infinity
        If timestamp.CompareTo(Double.NegativeInfinity) = 0 OrElse timestamp.CompareTo(Double.PositiveInfinity) = 0 Then
            Throw New ArgumentOutOfRangeException("timestamp", timestamp, "timestamp must be a valid discreet value.")
        End If
        'Values outside these ranges exceed the DateTime capacity of Jan 1, 0001 and Dec 31, 9999
        If timestamp > 253402300799.0R OrElse timestamp < -62135596800.0R Then
            Throw New ArgumentOutOfRangeException("timestamp", timestamp, "timestamp must resolve between Jan 1, 0001 and Dec 31, 9999")
        End If
        Dim origin As DateTime = New DateTime(1970, 1, 1, 0, 0, 0, 0)
        Return origin.AddSeconds(timestamp)
    End Function

    Public Shared Function ConvertStringToColor(ByVal value As String) As Color
        If Not String.IsNullOrEmpty(value) Then
            If Integer.TryParse(value, 0) Then
                Return Color.FromArgb(Convert.ToInt32(value))
            ElseIf Color.FromName(value).IsKnownColor OrElse value.StartsWith("#") Then
                Return ColorTranslator.FromHtml(value)
            Else
                logger.Error(String.Concat("No valid color value: ", value))
                Return New Color
            End If
        End If
    End Function
    ''' <summary>
    ''' Convert a VB-styled DateTime to a valid Unix-style timestamp
    ''' </summary>
    ''' <param name="data">A valid VB-style DateTime</param>
    ''' <returns>A value representing the DateTime as a unix-style timestamp <c>Double</c></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertToUnixTimestamp(ByVal data As DateTime) As Double
        Dim origin As Date = New DateTime(1970, 1, 1, 0, 0, 0, 0)
        Dim diff As TimeSpan = data - origin
        Return Math.Floor(diff.TotalSeconds)
    End Function

    Public Shared Function ConvertToProperDateTime(ByVal strDateTime As String) As String
        If String.IsNullOrEmpty(strDateTime) Then Return String.Empty

        Dim parsedDateTime As Date
        If Date.TryParse(strDateTime, parsedDateTime) Then
            Return parsedDateTime.ToString("yyyy-MM-dd HH:mm:ss")
        Else
            Return String.Empty
        End If
    End Function
    ''' <summary>
    ''' Sets the DoubleBuffered property for the supplied DataGridView.
    ''' This should have the effect of reducing any flicker during re-draw operations
    ''' </summary>
    ''' <param name="cDGV">DataGridView for which the <c>DoubleBuffered</c> property is to be set.</param>
    ''' <remarks></remarks>
    Public Shared Sub DGVDoubleBuffer(ByRef cDGV As DataGridView)
        Dim conType As Type = cDGV.GetType
        Dim pi As System.Reflection.PropertyInfo = conType.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
        pi.SetValue(cDGV, True, Nothing)
    End Sub
    ''' <summary>
    ''' Retrieves the Ember version
    ''' </summary>
    ''' <returns>A string representing the four-part period-separated version number</returns>
    ''' <remarks>An example of the string returned would be "1.4.0.0", without the quotes, of course</remarks>
    Public Shared Function EmberAPIVersion() As String
        Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion
    End Function

    ''' <summary>
    ''' Get the changelog for the latest version
    ''' </summary>
    ''' <returns>Changelog as string</returns>
    ''' <remarks>Not implemented yet. Always returns "Unavailable"</remarks>
    Public Shared Function GetChangelog() As String
        'TODO STUB - Not implemented yet

        'Try
        '    Dim sHTTP As New HTTP
        '    Dim strChangelog As String = sHTTP.DownloadData(String.Format("http://pcjco.dommel.be/emm-r/{0}/WhatsNew.txt", If(IsBetaEnabled(), "updatesbeta", "updates")))
        '    sHTTP = Nothing

        '    If strChangelog.Length > 0 Then
        '        Return strChangelog
        '    End If
        'Catch ex As Exception
        '    logger.Error(GetType(Functions),ex.Message, ex.StackTrace, "Error")
        'End Try
        Return "Unavailable"
    End Function

    ''' <summary>
    ''' Get the number of the last sequential extrathumb to make sure we're not overwriting current ones.
    ''' </summary>
    ''' <param name="sPath">Full path to extrathumbs directory</param>
    ''' <returns>Last detected number of the discovered extrathumbs.</returns>
    Public Shared Function GetExtrafanartsModifier(ByVal sPath As String) As Integer
        Dim iMod As Integer = 0
        Dim lThumbs As New List(Of String)

        Try
            If Directory.Exists(sPath) Then

                Try
                    lThumbs.AddRange(Directory.GetFiles(sPath, "extrafanart*.jpg"))
                Catch
                End Try

                If lThumbs.Count > 0 Then
                    Dim cur As Integer = 0
                    For Each t As String In lThumbs
                        cur = Convert.ToInt32(Regex.Match(t, "(\d+).jpg").Groups(1).ToString)
                        iMod = Math.Max(iMod, cur)
                    Next
                End If
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Failed trying to identify last Extrafanart from path: " & sPath)
        End Try

        Return iMod
    End Function

    ''' <summary>
    ''' Get the number of the last sequential extrathumb to make sure we're not overwriting current ones.
    ''' </summary>
    ''' <param name="sPath">Full path to extrathumbs directory</param>
    ''' <returns>Last detected number of the discovered extrathumbs.</returns>
    Public Shared Function GetExtrathumbsModifier(ByVal sPath As String) As Integer
        Dim iMod As Integer = 0
        Dim lThumbs As New List(Of String)

        Try
            If Directory.Exists(sPath) Then

                Try
                    lThumbs.AddRange(Directory.GetFiles(sPath, "thumb*.jpg"))
                Catch
                End Try

                If lThumbs.Count > 0 Then
                    Dim cur As Integer = 0
                    For Each t As String In lThumbs
                        cur = Convert.ToInt32(Regex.Match(t, "(\d+).jpg").Groups(1).ToString)
                        iMod = Math.Max(iMod, cur)
                    Next
                End If
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Failed trying to identify last Extrathumb from path: " & sPath)
        End Try

        Return iMod
    End Function
    ''' <summary>
    ''' Convert a List(of T) to a string of separated values
    ''' </summary>
    ''' <param name="source">List(of T)</param>
    ''' <param name="separator">Character or string to use as a value separator</param>
    ''' <returns>String of separated List values.
    ''' If the list is empty, an empty string will be returned.
    ''' If the separator is empty or missing, assume "," is the separator</returns>
    Public Shared Function ListToStringWithSeparator(Of T)(ByVal source As IList(Of T), ByVal separator As String) As String
        If source Is Nothing Then Return String.Empty
        If String.IsNullOrEmpty(separator) Then separator = ","

        Dim values As String() = source.Cast(Of Object)().Where(Function(n) n IsNot Nothing).Select(Function(s) s.ToString).ToArray

        Return String.Join(separator, values)
    End Function
    ''' <summary>
    ''' Set the DoubleBuffered property of the supplied Panel. This will tell the control to redraw its surface using a 
    ''' secondary buffer to reduce/prevent flicker.
    ''' </summary>
    ''' <param name="cPNL">Panel control to DoubleBuffer</param>
    ''' <remarks></remarks>
    Public Shared Sub PNLDoubleBuffer(ByRef cPNL As Panel)
        If cPNL Is Nothing Then Throw New ArgumentNullException("Source parameter cannot be nothing")
        Dim conType As Type = cPNL.GetType
        Dim pi As System.Reflection.PropertyInfo = conType.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
        pi.SetValue(cPNL, True, Nothing)
    End Sub

    ''' <summary>
    ''' Constrain a number to the nearest multiple. 
    ''' </summary>
    ''' <param name="iNumber">Number to quantize</param>
    ''' <param name="iMultiple">Multiple of constraint. This value can not be zero.</param>
    Public Shared Function Quantize(ByVal iNumber As Integer, ByVal iMultiple As Integer) As Integer
        If iMultiple = 0 Then Throw New ArgumentOutOfRangeException("Multiple must be greater than zero (0)")
        Return Convert.ToInt32(System.Math.Round(iNumber / iMultiple, 0) * iMultiple)
    End Function
    ''' <summary>
    ''' Reads bytes from a given stream and returns them as a Byte array
    ''' </summary>
    ''' <param name="rStream">Stream to be read from</param>
    ''' <returns>Byte array representing the contents of the supplied Stream</returns>
    ''' <remarks>Stream is read using a 4k buffer</remarks>
    Public Shared Function ReadStreamToEnd(ByVal rStream As Stream) As Byte()
        If rStream Is Nothing Then Throw New ArgumentNullException("Source parameter cannot be Nothing")
        Dim StreamBuffer(4096) As Byte
        Dim BlockSize As Integer = 0
        Using mStream As MemoryStream = New MemoryStream()
            Do
                BlockSize = rStream.Read(StreamBuffer, 0, StreamBuffer.Length)
                If BlockSize > 0 Then mStream.Write(StreamBuffer, 0, BlockSize)
            Loop While BlockSize > 0
            Return mStream.ToArray
        End Using
    End Function
    ''' <summary>
    ''' Determine the Structures.MovieScrapeOptions options that are in common between the two parameters
    ''' </summary>
    ''' <param name="Options">Base Structures.MovieScrapeOptions</param>
    ''' <param name="Options2">Secondary Structures.MovieScrapeOptions</param>
    ''' <returns>Structures.MovieScrapeOptions representing the AndAlso union of the two parameters</returns>
    ''' <remarks></remarks>
    Public Shared Function ScrapeOptionsAndAlso(ByVal Options As Structures.ScrapeOptions, ByVal Options2 As Structures.ScrapeOptions) As Structures.ScrapeOptions
        Dim FilteredOptions As New Structures.ScrapeOptions
        FilteredOptions.bEpisodeActors = Options.bEpisodeActors AndAlso Options2.bEpisodeActors
        FilteredOptions.bEpisodeAired = Options.bEpisodeAired AndAlso Options2.bEpisodeAired
        FilteredOptions.bEpisodeCredits = Options.bEpisodeCredits AndAlso Options2.bEpisodeCredits
        FilteredOptions.bEpisodeDirectors = Options.bEpisodeDirectors AndAlso Options2.bEpisodeDirectors
        FilteredOptions.bEpisodeGuestStars = Options.bEpisodeGuestStars AndAlso Options2.bEpisodeGuestStars
        FilteredOptions.bEpisodePlot = Options.bEpisodePlot AndAlso Options2.bEpisodePlot
        FilteredOptions.bEpisodeRating = Options.bEpisodeRating AndAlso Options2.bEpisodeRating
        FilteredOptions.bEpisodeRuntime = Options.bEpisodeRuntime AndAlso Options2.bEpisodeRuntime
        FilteredOptions.bEpisodeTitle = Options.bEpisodeTitle AndAlso Options2.bEpisodeTitle
        FilteredOptions.bEpisodeUserRating = Options.bEpisodeUserRating AndAlso Options2.bEpisodeUserRating
        FilteredOptions.bMainActors = Options.bMainActors AndAlso Options2.bMainActors
        FilteredOptions.bMainCertifications = Options.bMainCertifications AndAlso Options2.bMainCertifications
        FilteredOptions.bMainCollectionID = Options.bMainCollectionID AndAlso Options2.bMainCollectionID
        FilteredOptions.bMainCountries = Options.bMainCountries AndAlso Options2.bMainCountries
        FilteredOptions.bMainCreators = Options.bMainCreators AndAlso Options2.bMainCreators
        FilteredOptions.bMainDirectors = Options.bMainDirectors AndAlso Options2.bMainDirectors
        FilteredOptions.bMainEpisodeGuide = Options.bMainEpisodeGuide AndAlso Options2.bMainEpisodeGuide
        FilteredOptions.bMainGenres = Options.bMainGenres AndAlso Options2.bMainGenres
        FilteredOptions.bMainMPAA = Options.bMainMPAA AndAlso Options2.bMainMPAA
        FilteredOptions.bMainOriginalTitle = Options.bMainOriginalTitle AndAlso Options2.bMainOriginalTitle
        FilteredOptions.bMainOutline = Options.bMainOutline AndAlso Options2.bMainOutline
        FilteredOptions.bMainPlot = Options.bMainPlot AndAlso Options2.bMainPlot
        FilteredOptions.bMainPremiered = Options.bMainPremiered AndAlso Options2.bMainPremiered
        FilteredOptions.bMainRating = Options.bMainRating AndAlso Options2.bMainRating
        FilteredOptions.bMainRelease = Options.bMainRelease AndAlso Options2.bMainRelease
        FilteredOptions.bMainRuntime = Options.bMainRuntime AndAlso Options2.bMainRuntime
        FilteredOptions.bMainStatus = Options.bMainStatus AndAlso Options2.bMainStatus
        FilteredOptions.bMainStudios = Options.bMainStudios AndAlso Options2.bMainStudios
        FilteredOptions.bMainTagline = Options.bMainTagline AndAlso Options2.bMainTagline
        FilteredOptions.bMainTitle = Options.bMainTitle AndAlso Options2.bMainTitle
        FilteredOptions.bMainTop250 = Options.bMainTop250 AndAlso Options2.bMainTop250
        FilteredOptions.bMainTrailer = Options.bMainTrailer AndAlso Options2.bMainTrailer
        FilteredOptions.bMainUserRating = Options.bMainUserRating AndAlso Options2.bMainUserRating
        FilteredOptions.bMainWriters = Options.bMainWriters AndAlso Options2.bMainWriters
        FilteredOptions.bMainYear = Options.bMainYear AndAlso Options2.bMainYear
        FilteredOptions.bSeasonAired = Options.bSeasonAired AndAlso Options2.bSeasonAired
        FilteredOptions.bSeasonPlot = Options.bSeasonPlot AndAlso Options2.bSeasonPlot
        FilteredOptions.bSeasonTitle = Options.bSeasonTitle AndAlso Options2.bSeasonTitle
        Return FilteredOptions
    End Function

    Public Shared Function ScrapeModifiersAndAlso(ByVal Options As Structures.ScrapeModifiers, ByVal Options2 As Structures.ScrapeModifiers) As Structures.ScrapeModifiers
        Dim FilteredModifiers As New Structures.ScrapeModifiers
        FilteredModifiers.AllSeasonsBanner = Options.AllSeasonsBanner AndAlso Options2.AllSeasonsBanner
        FilteredModifiers.AllSeasonsFanart = Options.AllSeasonsFanart AndAlso Options2.AllSeasonsFanart
        FilteredModifiers.AllSeasonsLandscape = Options.AllSeasonsLandscape AndAlso Options2.AllSeasonsLandscape
        FilteredModifiers.AllSeasonsPoster = Options.AllSeasonsPoster AndAlso Options2.AllSeasonsPoster
        FilteredModifiers.DoSearch = Options.DoSearch AndAlso Options2.DoSearch
        FilteredModifiers.EpisodeActorThumbs = Options.EpisodeActorThumbs AndAlso Options2.EpisodeActorThumbs
        FilteredModifiers.EpisodeFanart = Options.EpisodeFanart AndAlso Options2.EpisodeFanart
        FilteredModifiers.EpisodeMeta = Options.EpisodeMeta AndAlso Options2.EpisodeMeta
        FilteredModifiers.EpisodeNFO = Options.EpisodeNFO AndAlso Options2.EpisodeNFO
        FilteredModifiers.EpisodePoster = Options.EpisodePoster AndAlso Options2.EpisodePoster
        FilteredModifiers.EpisodeSubtitles = Options.EpisodeSubtitles AndAlso Options2.EpisodeSubtitles
        FilteredModifiers.EpisodeWatchedFile = Options.EpisodeWatchedFile AndAlso Options2.EpisodeWatchedFile
        FilteredModifiers.MainActorthumbs = Options.MainActorthumbs AndAlso Options2.MainActorthumbs
        FilteredModifiers.MainBanner = Options.MainBanner AndAlso Options2.MainBanner
        FilteredModifiers.MainCharacterArt = Options.MainCharacterArt AndAlso Options2.MainCharacterArt
        FilteredModifiers.MainClearArt = Options.MainClearArt AndAlso Options2.MainClearArt
        FilteredModifiers.MainClearLogo = Options.MainClearLogo AndAlso Options2.MainClearLogo
        FilteredModifiers.MainDiscArt = Options.MainDiscArt AndAlso Options2.MainDiscArt
        FilteredModifiers.MainExtrafanarts = Options.MainExtrafanarts AndAlso Options2.MainExtrafanarts
        FilteredModifiers.MainExtrathumbs = Options.MainExtrathumbs AndAlso Options2.MainExtrathumbs
        FilteredModifiers.MainFanart = Options.MainFanart AndAlso Options2.MainFanart
        FilteredModifiers.MainKeyArt = Options.MainKeyArt AndAlso Options2.MainKeyArt
        FilteredModifiers.MainLandscape = Options.MainLandscape AndAlso Options2.MainLandscape
        FilteredModifiers.MainNFO = Options.MainNFO AndAlso Options2.MainNFO
        FilteredModifiers.MainPoster = Options.MainPoster AndAlso Options2.MainPoster
        FilteredModifiers.MainSubtitles = Options.MainSubtitles AndAlso Options2.MainSubtitles
        FilteredModifiers.MainTheme = Options.MainTheme AndAlso Options2.MainTheme
        FilteredModifiers.MainTrailer = Options.MainTrailer AndAlso Options2.MainTrailer
        FilteredModifiers.MainWatchedFile = Options.MainWatchedFile AndAlso Options2.MainWatchedFile
        FilteredModifiers.SeasonBanner = Options.SeasonBanner AndAlso Options2.SeasonBanner
        FilteredModifiers.SeasonFanart = Options.SeasonFanart AndAlso Options2.SeasonFanart
        FilteredModifiers.SeasonLandscape = Options.SeasonLandscape AndAlso Options2.SeasonLandscape
        FilteredModifiers.SeasonNFO = Options.SeasonNFO AndAlso Options2.SeasonNFO
        FilteredModifiers.SeasonPoster = Options.SeasonPoster AndAlso Options2.SeasonPoster
        FilteredModifiers.withEpisodes = Options.withEpisodes AndAlso Options2.withEpisodes
        FilteredModifiers.withSeasons = Options.withSeasons AndAlso Options2.withSeasons
        Return FilteredModifiers
    End Function

    Public Shared Function ScrapeModifiersAnyEnabled(ByVal Options As Structures.ScrapeModifiers) As Boolean
        With Options
            If .AllSeasonsBanner OrElse
                .AllSeasonsFanart OrElse
                .AllSeasonsLandscape OrElse
                .AllSeasonsPoster OrElse
                .EpisodeActorThumbs OrElse
                .EpisodeFanart OrElse
                .EpisodeMeta OrElse
                .EpisodeNFO OrElse
                .EpisodePoster OrElse
                .EpisodeSubtitles OrElse
                .EpisodeWatchedFile OrElse
                .MainActorthumbs OrElse
                .MainBanner OrElse
                .MainCharacterArt OrElse
                .MainClearArt OrElse
                .MainClearLogo OrElse
                .MainDiscArt OrElse
                .MainExtrafanarts OrElse
                .MainExtrathumbs OrElse
                .MainFanart OrElse
                .MainKeyArt OrElse
                .MainLandscape OrElse
                .MainMeta OrElse
                .MainNFO OrElse
                .MainPoster OrElse
                .MainSubtitles OrElse
                .MainTheme OrElse
                .MainTrailer OrElse
                .MainWatchedFile OrElse
                .SeasonBanner OrElse
                .SeasonFanart OrElse
                .SeasonLandscape OrElse
                .SeasonNFO OrElse
                .SeasonPoster Then
                Return True
            End If
        End With
        Return False
    End Function

    Public Shared Sub SetScrapeModifiers(ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByVal MType As Enums.ModifierType, ByVal MValue As Boolean)
        With ScrapeModifiers
            Select Case MType
                Case Enums.ModifierType.All
                    .AllSeasonsBanner = MValue
                    .AllSeasonsFanart = MValue
                    .AllSeasonsLandscape = MValue
                    .AllSeasonsPoster = MValue
                    '.DoSearch should not be set here as it is only needed for a re-search of a movie (first scraping or movie change).
                    .EpisodeActorThumbs = MValue
                    .EpisodeFanart = MValue
                    .EpisodeMeta = MValue
                    .EpisodeNFO = MValue
                    .EpisodePoster = MValue
                    .EpisodeSubtitles = MValue
                    .EpisodeWatchedFile = MValue
                    .MainActorthumbs = MValue
                    .MainBanner = MValue
                    .MainCharacterArt = MValue
                    .MainClearArt = MValue
                    .MainClearLogo = MValue
                    .MainDiscArt = MValue
                    .MainExtrafanarts = MValue
                    .MainExtrathumbs = MValue
                    .MainFanart = MValue
                    .MainKeyArt = MValue
                    .MainLandscape = MValue
                    .MainMeta = MValue
                    .MainNFO = MValue
                    .MainPoster = MValue
                    .MainSubtitles = MValue
                    .MainTheme = MValue
                    .MainTrailer = MValue
                    .MainWatchedFile = MValue
                    .SeasonBanner = MValue
                    .SeasonFanart = MValue
                    .SeasonLandscape = MValue
                    .SeasonNFO = MValue
                    .SeasonPoster = MValue
                '.withEpisodes should not be set here
                '.withSeasons should not be set here
                Case Enums.ModifierType.AllSeasonsBanner
                    .AllSeasonsBanner = MValue
                Case Enums.ModifierType.AllSeasonsFanart
                    .AllSeasonsFanart = MValue
                Case Enums.ModifierType.AllSeasonsLandscape
                    .AllSeasonsLandscape = MValue
                Case Enums.ModifierType.AllSeasonsPoster
                    .AllSeasonsPoster = MValue
                Case Enums.ModifierType.DoSearch
                    .DoSearch = MValue
                Case Enums.ModifierType.EpisodeActorThumbs
                    .EpisodeActorThumbs = MValue
                Case Enums.ModifierType.EpisodeFanart
                    .EpisodeFanart = MValue
                Case Enums.ModifierType.EpisodeMeta
                    .EpisodeMeta = MValue
                Case Enums.ModifierType.EpisodeNFO
                    .EpisodeNFO = MValue
                Case Enums.ModifierType.EpisodePoster
                    .EpisodePoster = MValue
                Case Enums.ModifierType.EpisodeSubtitle
                    .EpisodeSubtitles = MValue
                Case Enums.ModifierType.EpisodeWatchedFile
                    .EpisodeWatchedFile = MValue
                Case Enums.ModifierType.MainActorThumbs
                    .MainActorthumbs = MValue
                Case Enums.ModifierType.MainBanner
                    .MainBanner = MValue
                Case Enums.ModifierType.MainCharacterArt
                    .MainCharacterArt = MValue
                Case Enums.ModifierType.MainClearArt
                    .MainClearArt = MValue
                Case Enums.ModifierType.MainClearLogo
                    .MainClearLogo = MValue
                Case Enums.ModifierType.MainDiscArt
                    .MainDiscArt = MValue
                Case Enums.ModifierType.MainExtrafanarts
                    .MainExtrafanarts = MValue
                Case Enums.ModifierType.MainExtrathumbs
                    .MainExtrathumbs = MValue
                Case Enums.ModifierType.MainFanart
                    .MainFanart = MValue
                Case Enums.ModifierType.MainKeyArt
                    .MainKeyArt = MValue
                Case Enums.ModifierType.MainLandscape
                    .MainLandscape = MValue
                Case Enums.ModifierType.MainMeta
                    .MainMeta = MValue
                Case Enums.ModifierType.MainNFO
                    .MainNFO = MValue
                Case Enums.ModifierType.MainPoster
                    .MainPoster = MValue
                Case Enums.ModifierType.MainSubtitle
                    .MainSubtitles = MValue
                Case Enums.ModifierType.MainTheme
                    .MainTheme = MValue
                Case Enums.ModifierType.MainTrailer
                    .MainTrailer = MValue
                Case Enums.ModifierType.MainWatchedFile
                    .MainWatchedFile = MValue
                Case Enums.ModifierType.SeasonBanner
                    .SeasonBanner = MValue
                Case Enums.ModifierType.SeasonFanart
                    .SeasonFanart = MValue
                Case Enums.ModifierType.SeasonLandscape
                    .SeasonLandscape = MValue
                Case Enums.ModifierType.SeasonNFO
                    .SeasonNFO = MValue
                Case Enums.ModifierType.SeasonPoster
                    .SeasonPoster = MValue
                Case Enums.ModifierType.withEpisodes
                    .withEpisodes = MValue
                Case Enums.ModifierType.withSeasons
                    .withSeasons = MValue
            End Select
        End With
    End Sub
    ''' <summary>
    ''' This method launches the user's default web browser to the supplied destination
    ''' </summary>
    ''' <param name="Destination">URL or file to be launched. Note that care should be taken when launching files, as it exposes
    ''' the user to a high security risk.</param>
    ''' <param name="AllowLocalFiles">If <c>False</c>, no action will be taken if the destination points to a local file.
    ''' This protects the user's machine from malformed URLs</param>
    ''' <returns><c>True</c> if process was launched, or <c>False</c> if an error prevented the launch from occurring.
    ''' Note that a process may be launched but produce no visible results. This flag only indicates that the process was called.</returns>
    ''' <remarks>Note that if the supplied string is not a valid URI, 
    ''' or if it refers to a local file,
    ''' a log message will be generated but no further action will be taken.
    ''' This is to prevent malformed URIs from attacking the user's machine.</remarks>
    Public Shared Function Launch(ByRef Destination As String, Optional ByRef AllowLocalFiles As Boolean = False) As Boolean
        If String.IsNullOrEmpty(Destination) Then
            logger.Error("Blank destination")
            Return False
        End If
        Try
            Dim uriDestination As New Uri(Destination)
            If uriDestination.IsFile() Then
                If (Not AllowLocalFiles) Then
                    logger.Error("Destination is a file, which is not permitted for security reasons <{0}>", Destination)
                    Return False
                Else
                    If (Not File.Exists(uriDestination.LocalPath)) Then
                        logger.Error("Destination is a file, but it does not exist <{0}>", Destination)
                        Return False
                    Else
                        Process.Start(uriDestination.LocalPath)
                        Return True
                    End If
                End If
            End If

            'If we got this far, everything is OK, so we can go ahead and launch it!
            Process.Start(uriDestination.AbsoluteUri())
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Could not launch <" & Destination & ">")
            Return False
        End Try
        'If you got here, everything went fine
        Return True
    End Function

    ''' <summary>
    ''' Run console commands from Ember (used for calling mediainfo-rar.exe for scanning ISO files)
    ''' </summary>
    ''' <param name="Process_Name">The name i.e "vcmount.exe" of the process</param>
    ''' <param name="Process_Arguments">Optional arguments, i.e "/u"</param>
    ''' <param name="Read_Output">If <c>True</c>, returns console outputs like mediainfo-rar.exe scanresults </param>
    ''' <param name="Process_Hide">If <c>True</c>, hide console window </param>
    ''' <param name="Process_TimeOut">Timeout value - closes after specific time frame</param>
    Public Shared Function Run_Process(ByVal Process_Name As String, Optional Process_Arguments As String = Nothing, Optional Read_Output As Boolean = False, Optional Process_Hide As Boolean = False, Optional Process_TimeOut As Integer = 30000) As String

        Dim OutputString As String = String.Empty

        Try
            Using My_Process As New Process()
                AddHandler My_Process.OutputDataReceived, Sub(sender As Object, LineOut As DataReceivedEventArgs)
                                                              OutputString = OutputString & LineOut.Data ' & vbCrLf
                                                          End Sub

                Dim My_Process_Info As New ProcessStartInfo()
                My_Process_Info.FileName = Process_Name ' Process filename
                My_Process_Info.Arguments = Process_Arguments ' Process arguments
                My_Process_Info.CreateNoWindow = Process_Hide ' Show or hide the process Window
                My_Process_Info.UseShellExecute = False ' Don't use system shell to execute the process
                My_Process_Info.RedirectStandardOutput = Read_Output '  Redirect Output
                My_Process_Info.RedirectStandardError = Read_Output ' Redirect non Output
                My_Process.EnableRaisingEvents = True ' Raise events
                My_Process.StartInfo = My_Process_Info

                My_Process.Start() ' Run the process NOW

                If Read_Output = True Then
                    System.Threading.Thread.Sleep(500)
                    My_Process.BeginOutputReadLine()
                    System.Threading.Thread.Sleep(1000)
                    Do
                        'TODO?!
                    Loop Until My_Process.HasExited
                End If
                '    RemoveHandler My_Process.OutputDataReceived, AddressOf proc_DataReceived
                My_Process.WaitForExit(Process_TimeOut) ' Wait X ms to kill the process (Default value is 30000 ms)
                My_Process.Close()
            End Using
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Could not launch <" & Process_Name & ">")
        End Try

        Return OutputString
    End Function
#End Region 'Methods

End Class 'Functions

Public Class Structures

#Region "Nested Types"

    Public Structure CustomUpdaterStruct
        Dim Canceled As Boolean
        Dim ScrapeOptions As ScrapeOptions
        Dim ScrapeModifiers As ScrapeModifiers
        Dim ScrapeType As Enums.ScrapeType
    End Structure
    ''' <summary>
    ''' Structure representing a tag in the database
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure DBMovieTag
        Dim ID As Long
        Dim Movies As List(Of Database.DBElement)
        Dim Title As String
    End Structure

    Public Structure ScanOrClean
        Dim Movies As Boolean
        Dim MovieSets As Boolean
        Dim SpecificFolder As Boolean
        Dim TV As Boolean
    End Structure

    Public Structure ScrapeModifiers
        Dim AllSeasonsBanner As Boolean
        Dim AllSeasonsFanart As Boolean
        Dim AllSeasonsLandscape As Boolean
        Dim AllSeasonsPoster As Boolean
        Dim DoSearch As Boolean
        Dim EpisodeActorThumbs As Boolean
        Dim EpisodeFanart As Boolean
        Dim EpisodePoster As Boolean
        Dim EpisodeMeta As Boolean
        Dim EpisodeNFO As Boolean
        Dim EpisodeSubtitles As Boolean
        Dim EpisodeWatchedFile As Boolean
        Dim MainActorthumbs As Boolean
        Dim MainBanner As Boolean
        Dim MainCharacterArt As Boolean
        Dim MainClearArt As Boolean
        Dim MainClearLogo As Boolean
        Dim MainDiscArt As Boolean
        Dim MainExtrafanarts As Boolean
        Dim MainExtrathumbs As Boolean
        Dim MainFanart As Boolean
        Dim MainKeyArt As Boolean
        Dim MainLandscape As Boolean
        Dim MainMeta As Boolean
        Dim MainNFO As Boolean
        Dim MainPoster As Boolean
        Dim MainSubtitles As Boolean
        Dim MainTheme As Boolean
        Dim MainTrailer As Boolean
        Dim MainWatchedFile As Boolean
        Dim SeasonBanner As Boolean
        Dim SeasonFanart As Boolean
        Dim SeasonLandscape As Boolean
        Dim SeasonNFO As Boolean
        Dim SeasonPoster As Boolean
        Dim withEpisodes As Boolean
        Dim withSeasons As Boolean
    End Structure
    ''' <summary>
    ''' Structure representing posible scrape fields
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Structure ScrapeOptions
        Dim bEpisodeActors As Boolean
        Dim bEpisodeAired As Boolean
        Dim bEpisodeCredits As Boolean
        Dim bEpisodeDirectors As Boolean
        Dim bEpisodeGuestStars As Boolean
        Dim bEpisodeOriginalTitle As Boolean
        Dim bEpisodePlot As Boolean
        Dim bEpisodeRating As Boolean
        Dim bEpisodeRuntime As Boolean
        Dim bEpisodeTitle As Boolean
        Dim bEpisodeUserRating As Boolean
        Dim bEpisodeVideoSource As Boolean
        Dim bMainActors As Boolean
        Dim bMainCertifications As Boolean
        Dim bMainCollectionID As Boolean
        Dim bMainCountries As Boolean
        Dim bMainCreators As Boolean
        Dim bMainDirectors As Boolean
        Dim bMainEpisodeGuide As Boolean
        Dim bMainGenres As Boolean
        Dim bMainMPAA As Boolean
        Dim bMainOriginalTitle As Boolean
        Dim bMainOutline As Boolean
        Dim bMainPlot As Boolean
        Dim bMainPremiered As Boolean
        Dim bMainRating As Boolean
        Dim bMainRelease As Boolean
        Dim bMainRuntime As Boolean
        Dim bMainStatus As Boolean
        Dim bMainStudios As Boolean
        Dim bMainTagline As Boolean
        Dim bMainTags As Boolean
        Dim bMainTitle As Boolean
        Dim bMainTop250 As Boolean
        Dim bMainTrailer As Boolean
        Dim bMainUserRating As Boolean
        Dim bMainVideoSource As Boolean
        Dim bMainWriters As Boolean
        Dim bMainYear As Boolean
        Dim bSeasonAired As Boolean
        Dim bSeasonPlot As Boolean
        Dim bSeasonTitle As Boolean
    End Structure

    Public Structure SettingsResult
        Dim DidCancel As Boolean
        Dim NeedsDBClean_Movie As Boolean
        Dim NeedsDBClean_TV As Boolean
        Dim NeedsDBUpdate_Movie As Boolean
        Dim NeedsDBUpdate_TV As Boolean
        Dim NeedsReload_Movie As Boolean
        Dim NeedsReload_MovieSet As Boolean
        Dim NeedsReload_TVEpisode As Boolean
        Dim NeedsReload_TVShow As Boolean
        Dim NeedsRestart As Boolean
    End Structure

    Public Structure ModulesMenus
        Dim ForMovies As Boolean
        Dim ForMovieSets As Boolean
        Dim ForTVShows As Boolean
        Dim IfTabMovies As Boolean      'Only if Movies Tab is selected
        Dim IfTabMovieSets As Boolean   'Only if MovieSets Tab is selected
        Dim IfTabTVShows As Boolean     'Only if TV Shows Tab is selected
        Dim IfNoMovies As Boolean       'Show also if the Movies list is empty
        Dim IfNoMovieSets As Boolean    'Show also if the MovieSets list is empty
        Dim IfNoTVShows As Boolean      'Show also if the TV Shows list is empty
    End Structure

#End Region 'Nested Types

End Class 'Structures