# CityLearn: Diverse Real-World Environments for Sample-Efficient Navigation Policy Learning

Oxford RobotCar dataset

<img src="readme/night.gif" width="32%" height="60%" /> <img src="readme/snow.gif" width="32%" height="60%" /> <img src="readme/ovc.gif" width="32%" height="60%" />

Nordland Railway dataset

<img src="readme/summer.gif" width="32%" height="60%" /> <img src="readme/fall.gif" width="32%" height="60%" /> <img src="readme/winter.gif" width="32%" height="60%" />

We provide the **CityLearn** environment proposed in [**CityLearn: Diverse Real-World Environments for Sample-Efficient Navigation Policy Learning**](https://arxiv.org/pdf/1910.04335.pdf), accepted for publication in the IEEE International Conference on Robotics and Automation (ICRA 2020). Preprint version available at https://arxiv.org/abs/1910.04335.

Project page: https://mchancan.github.io/projects/CityLearn

## About CityLearn

CityLearn is an interactive open framework for training and testing navigation algorithms on real-world environments with extreme visual appearance changes including day to night or summer to winter transitions. We leverage publicly available datasets, often used in visual place recognition and autonomous vehicles research, consisting of multiple traversals across different seasons, time of day or weather conditions. CityLearn is also designed to test the generalization capabilities of navigation algorithms including reinforcement learning agents.

## Installation

### Requirements

CityLearn is developed on top of the Unity ML-Agents toolkit, which runs on Mac OS X, Windows, or Linux.

Dependencies:

* Python 3.6
* Unity game engine
* Unity ML-Agents toolkit

### Configuring and using CityLearn!

1. Download and install [Unity](https://unity.com/products/core-platform) 2017.4 from [here](https://unity3d.com/get-unity/download/archive) or through [UnityHub](https://store.unity.com/download).

2. Download and install [Unity ML-Agents v0.8.1](https://github.com/Unity-Technologies/ml-agents/releases/tag/0.8.1). Using other [ML-Agent](https://github.com/Unity-Technologies/ml-agents) releases may require substantial [changes](https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Migrating.md) as CityLearn was developed using v0.8.1. Once downloaded, you will need to install this for development. To do so, from the `ml-agents` repository's root directory, run:

	pip3 install -e ./ml-agents-envs
	pip3 install -e ./ml-agents

3. Clone this repository on a temporal folder:

	```bash
	git clone https://github.com/mchancan/citylearn.git
	```
- Put the `CityLearn` directory inside `UnitySDK/Assets/ML-Agents/Examples/` in your `ml-agents-0.8.1` directory.
- Add the files provided in the `config` directory inside `config` in your `ml-agents-0.8.1` directory.

4. Download the driving datasets of your choice:

- Oxford RobotCar: https://robotcar-dataset.robots.ox.ac.uk/
- Berkeley DeepDrive: https://bdd-data.berkeley.edu/
- Cityspaces: https://www.cityscapes-dataset.com/
- Kitti: http://www.cvlibs.net/datasets/kitti/index.php
- Nordland railway: https://nrkbeta.no/2013/01/15/nordlandsbanen-minute-by-minute-season-by-season/
- Multi-lane Road (videos): https://wiki.qut.edu.au/display/raq/2014+Multi-Lane+Road+Sideways-Camera+Datasets
- Gold Coast Drive (video): https://wiki.qut.edu.au/display/raq/Datasets
- UQ St Lucia: https://wiki.qut.edu.au/display/raq/UQ+St+Lucia
- St Lucia Multiple Times of Day (videos): https://wiki.qut.edu.au/display/raq/St+Lucia+Multiple+Times+of+Day
- Alderley (video+frames): https://wiki.qut.edu.au/pages/viewpage.action?pageId=181178395

5. Run a demo! The code provided in the `CityLearn` directory can be directly used on this subset of the [Nordland dataset](https://drive.google.com/drive/folders/1xrHKrHYgSqrMk9-XeC1qIe8UYDmOsgfd), but you can easily use any other driving dataset. For extracting frames out of the downloaded videos, you can use [Avconv](https://libav.org/avconv.html) on Ubuntu, e.g.

## License

CityLearn itself is released under the MIT License (refer to the LICENSE file for details) for academic purposes. For commercial usage, please contact us via `mchancanl@uni.pe`


## Citation

If you find this project useful for your research, please use the following BibTeX entry.

	@article{
		chancan2020citylearn,
		author = {M. {Chanc\'an} and M. {Milford}},
		title = {From Visual Place Recognition to Navigation: Learning Sample-Efficient Control Policies across Diverse Real World Environments},
		journal = {arXiv preprint arXiv:1910.04335},
		year = {2019}
	}
